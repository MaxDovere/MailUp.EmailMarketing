namespace MailUp.EmailMarketing.Model
{
    /// <summary>The possibles states of a recipient</summary>
    public enum EnumSubscriptionStatus
    {
        /// <summary>
        /// A recipient that still needs to confirm his/her subscription by clicking on the confirmation email.
        /// This recipient is not receiving the messages.
        /// </summary>
        Pending,
        /// <summary>A recipient that currently receives the messages</summary>
        Subscribed,
        /// <summary>A recipient that doesn't receive messages</summary>
        Unsubscribed,
        /// <summary>Unknown status</summary>
        Unknown,
    }
}
