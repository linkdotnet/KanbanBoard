using System;
using System.Threading;
using System.Threading.Tasks;
using Blazored.Toast.Services;
using BlazorState;
using MediatR;

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
            catch (Exception e)
            {
                _toastService.ShowError($"There was an error: {e}");
                throw;
            }
        }

        protected abstract Task<Unit> InnerHandleAsync(TAction action, CancellationToken cancellationToken);
    }
}