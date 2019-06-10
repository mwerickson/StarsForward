using System;
using Xamarin.Forms;

namespace StarsForward.Behaviors
{
    public class FocusOnReturnBehavior : Behavior<Entry>
    {
        public VisualElement FocusOn { get; set; }

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.Completed += BindableOnCompleted;
        }


        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.Completed -= BindableOnCompleted;
        }

        private void BindableOnCompleted(object sender, EventArgs args)
        {
            FocusOn?.Focus();
        }
    }
}