using System;
using System.Threading;
using System.Threading.Tasks;
using Blazored.Toast.Services;
using BlazorState;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace LinkDotNet.KanbanBoard.UI.Features
{
    public abstract class ActionHandlerBase<TAction> : ActionHandler<TAction> 
        where TAction : IAction
    {
        private readonly IToastService _toastService;

        protected ActionHandlerBase(IStore aStore, IToastService toastService) : base(aStore)
        {
            _toastService = toastService;
        }

        public override async Task<Unit> Handle(TAction aAction, CancellationToken aCancellationToken)
        {
            try
            {
                return await InnerHandleAsync(aAction, aCancellationToken);
            }
            catch (RpcException rpcException)
            {
                _toastService.ShowError((RenderFragment) CreateErrorMessage, "Server-Error");
                throw;

                void CreateErrorMessage(RenderTreeBuilder builder)
                {
                    builder.AddMarkupContent(0, "There was an error while communicating with the server<br/>");
                    builder.AddMarkupContent(1, $"Status-Code: {rpcException.Status}");
                }
            }

            catch (Exception exc)
            {
                _toastService.ShowError($"Unknown Error: {exc.Message}");
                throw;
            }
        }

        protected abstract Task<Unit> InnerHandleAsync(TAction action, CancellationToken cancellationToken);
    }
}