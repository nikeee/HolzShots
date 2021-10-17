using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HolzShots.Net;
using Microsoft.Toolkit.Uwp.Notifications;

namespace HolzShots.Forms;

public static class ToastNotifications
{
    public static void ShowSettingsUpdated()
    {
        new ToastContentBuilder()
            .AddText("Settings Updated", AdaptiveTextStyle.Header)
            .AddText("HolzShots has detected and loaded new settings.", AdaptiveTextStyle.HeaderSubtle)
            .SetToastDuration(ToastDuration.Short)
            .Show(toast => toast.ExpirationTime = DateTime.Now.AddSeconds(1));
    }

}
