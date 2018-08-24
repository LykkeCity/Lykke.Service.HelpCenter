using System;
using System.Threading.Tasks;
using Common;
using Lykke.Common.Log;

namespace Lykke.Service.HelpCenter.PeriodicHandlers
{
    /// <summary>
    /// Periodic handler that synchronizes the user data between Lykke and ZenDesk so when an email adress changes at Lykke this is also updated in ZenDesk.
    /// </summary>
    /// <seealso cref="T:Common.TimerPeriod" />
    /// <inheritdoc />
    internal sealed class UserDataSyncronizer : TimerPeriod
    {
        public UserDataSyncronizer(TimeSpan period, ILogFactory logFactory)
            : base(period, logFactory)
        {
        }

        public override Task Execute()
        {
            // TODO check if email address in zendesk is still correct
            return Task.CompletedTask;
        }
    }
}
