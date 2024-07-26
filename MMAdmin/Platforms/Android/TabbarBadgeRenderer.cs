using System;
using Google.Android.Material.Badge;
using Google.Android.Material.BottomNavigation;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform.Compatibility;
using Microsoft.Maui.Platform;
using Android.Content;
using Android.Graphics.Drawables;
using AndroidX.Core.Content;
using Microsoft.Maui.Controls.Platform;

namespace MMAdmin
{
    public class TabbarBadgeRenderer : ShellRenderer
    {
        protected override IShellBottomNavViewAppearanceTracker CreateBottomNavViewAppearanceTracker(ShellItem shellItem)
        {
            return new BadgeShellBottomNavViewAppearanceTracker(this, shellItem);
        }
    }

    class BadgeShellBottomNavViewAppearanceTracker : ShellBottomNavViewAppearanceTracker
    {
        private BadgeDrawable? firstBadgeDrawable;
        private BadgeDrawable? secondBadgeDrawable;

        public BadgeShellBottomNavViewAppearanceTracker(IShellContext shellContext, ShellItem shellItem) : base(shellContext, shellItem)
        {
        }

        public override void SetAppearance(BottomNavigationView bottomView, IShellAppearanceElement appearance)
        {
            base.SetAppearance(bottomView, appearance);

            InitializeBadges(bottomView);
            UpdateTabIcons(bottomView);
        }

        private void InitializeBadges(BottomNavigationView bottomView)
        {
            const int firstTabbarItemIndex = 1; // Index of the first tab you want to show a badge on
            const int secondTabbarItemIndex = 2; // Index of the second tab you want to show a badge on

            firstBadgeDrawable = bottomView.GetOrCreateBadge(firstTabbarItemIndex);
            secondBadgeDrawable = bottomView.GetOrCreateBadge(secondTabbarItemIndex);

            BadgeCounterService.CountChanged += OnFirstBadgeCountChanged;
            BadgeCounterService.SecondCountChanged += OnSecondBadgeCountChanged;

            UpdateFirstBadge(BadgeCounterService.Count);
            UpdateSecondBadge(BadgeCounterService.SecondCount);
        }

        private void OnFirstBadgeCountChanged(object? sender, int newCount)
        {
            UpdateFirstBadge(newCount);
        }

        private void OnSecondBadgeCountChanged(object? sender, int newCount)
        {
            UpdateSecondBadge(newCount);
        }

        private void UpdateFirstBadge(int count)
        {
            if (firstBadgeDrawable is not null)
            {
                firstBadgeDrawable.SetVisible(count > 0);
                if (count > 0)
                {
                    firstBadgeDrawable.Number = count;
                    firstBadgeDrawable.BackgroundColor = Colors.Red.ToPlatform();
                    firstBadgeDrawable.BadgeTextColor = Colors.White.ToPlatform();
                }
            }
        }

        private void UpdateSecondBadge(int count)
        {
            if (secondBadgeDrawable is not null)
            {
                secondBadgeDrawable.SetVisible(count > 0);
                if (count > 0)
                {
                    secondBadgeDrawable.Number = count;
                    secondBadgeDrawable.BackgroundColor = Colors.Red.ToPlatform(); // Same color as the first badge
                    secondBadgeDrawable.BadgeTextColor = Colors.White.ToPlatform();
                }
            }
        }

        private void UpdateTabIcons(BottomNavigationView bottomView)
        {
            var context = bottomView.Context;
            for (int i = 0; i < bottomView.Menu.Size(); i++)
            {
                var menuItem = bottomView.Menu.GetItem(i);
                bool isSelected = bottomView.SelectedItemId == menuItem.ItemId;
                menuItem.SetIcon(GetTabIcon(context, i, isSelected));
            }
        }

        private Drawable GetTabIcon(Context context, int index, bool isSelected)
        {
            string resourceName = GetResourceName(index, isSelected);
            int resourceId = context.Resources.GetIdentifier(resourceName, "drawable", context.PackageName);
            return ContextCompat.GetDrawable(context, resourceId);
        }

        private string GetResourceName(int index, bool isSelected)
        {
            switch (index)
            {
                case 0:
                    return isSelected ? "icon_dashboard" : "icon_dashboard_black";
                case 1:
                    return isSelected ? "selectedshop" : "shop";
                case 2:
                    return isSelected ? "icon_notification" : "icon_notification_black";
                case 3:
                    return isSelected ? "icon_leads" : "icon_leads_black";
                case 4:
                    return isSelected ? "icon_schedule" : "icon_schedule_black";
                default:
                    return "default_icon";
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            BadgeCounterService.CountChanged -= OnFirstBadgeCountChanged;
            BadgeCounterService.SecondCountChanged -= OnSecondBadgeCountChanged;
        }
    }
}
