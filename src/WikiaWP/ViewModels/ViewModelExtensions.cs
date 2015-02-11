using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

using Microsoft.Phone.Shell;

using MVVMSidekick.ViewModels;

namespace WikiaWP.ViewModels
{
    public static class ViewModelExtensions
    {
        public static readonly TimeSpan DefaultShowToastMessageTime = TimeSpan.FromSeconds(2);

        public static async Task ShowToastMessage<TViewModel>(this ViewModelBase<TViewModel> vm, string message, TimeSpan time = default(TimeSpan))
            where TViewModel : ViewModelBase<TViewModel>
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            if (time < TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException("time");
            }

            if (time == TimeSpan.Zero)
            {
                time = DefaultShowToastMessageTime;

            }
            await vm.Dispatcher.InvokeAsync(
                async () =>
                {
                    var currentIndicator = SystemTray.ProgressIndicator;
                    var currentColor = SystemTray.BackgroundColor;
                    var newIndicator = new ProgressIndicator
                    {
                        Text = message,
                        IsIndeterminate = false,
                        IsVisible = true
                    };
                    SystemTray.BackgroundColor =
                        (Color)Application.Current.Resources["PhoneAccentColor"];
                    SystemTray.ProgressIndicator = newIndicator;
                    await Task.Delay(time);
                    SystemTray.BackgroundColor = currentColor;
                    SystemTray.ProgressIndicator = currentIndicator;
                });
        }
    }
}