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
        private BadgeDrawable? _firstBadgeDrawable;
        private BadgeDrawable? _secondBadgeDrawable;

        public BadgeShellBottomNavViewAppearanceTracker(IShellContext shellContext, ShellItem shellItem) 
            : base(shellContext, shellItem)
        {
        }

        public override void SetAppearance(BottomNavigationView bottomView, IShellAppearanceElement appearance)
        {
            base.SetAppearance(bottomView, appearance);

            if (_firstBadgeDrawable == null || _secondBadgeDrawable == null)
            {
                InitializeBadges(bottomView);
            }

            UpdateTabIcons(bottomView);
        }

        private void InitializeBadges(BottomNavigationView bottomView)
        {
            const int firstTabbarItemIndex = 1; // Index of the first tab to show badge
            const int secondTabbarItemIndex = 2; // Index of the second tab to show badge

            _firstBadgeDrawable = bottomView.GetOrCreateBadge(firstTabbarItemIndex);
            _secondBadgeDrawable = bottomView.GetOrCreateBadge(secondTabbarItemIndex);

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
            if (_firstBadgeDrawable is not null)
            {
                _firstBadgeDrawable.SetVisible(count > 0);
                _firstBadgeDrawable.Number = count > 0 ? count : 0;
                _firstBadgeDrawable.BackgroundColor = Colors.Red.ToPlatform();
                _firstBadgeDrawable.BadgeTextColor = Colors.White.ToPlatform();
            }
        }

        private void UpdateSecondBadge(int count)
        {
            if (_secondBadgeDrawable is not null)
            {
                _secondBadgeDrawable.SetVisible(count > 0);
                _secondBadgeDrawable.Number = count > 0 ? count : 0;
                _secondBadgeDrawable.BackgroundColor = Colors.Blue.ToPlatform(); // Customizable color
                _secondBadgeDrawable.BadgeTextColor = Colors.White.ToPlatform();
            }
        }

        private void UpdateTabIcons(BottomNavigationView bottomView)
        {
            var context = bottomView.Context;
            for (int i = 0; i < bottomView.Menu.Size(); i++)
            {
                var menuItem = bottomView.Menu.GetItem(i);
                bool isSelected = bottomView.SelectedItemId == menuItem.ItemId;

                var drawable = GetTabIcon(context, i, isSelected);
                menuItem.SetIcon(drawable);
            }
        }

        private Drawable GetTabIcon(Context context, int index, bool isSelected)
        {
            string resourceName = GetResourceName(index, isSelected);
            int resourceId = context.Resources.GetIdentifier(resourceName, "drawable", context.PackageName);

            return resourceId != 0 
                ? ContextCompat.GetDrawable(context, resourceId) 
                : ContextCompat.GetDrawable(context, Resource.Drawable.design_fab_background)!;
        }

        private string GetResourceName(int index, bool isSelected)
        {
            return index switch
            {
                0 => isSelected ? "icon_dashboard" : "icon_dashboard_black",
                1 => isSelected ? "selectedshop" : "shop",
                2 => isSelected ? "selectedproduct" : "product",
                3 => isSelected ? "icon_leads" : "icon_leads_black",
                4 => isSelected ? "icon_schedule" : "icon_schedule_black",
                _ => "default_icon",
            };
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                BadgeCounterService.CountChanged -= OnFirstBadgeCountChanged;
                BadgeCounterService.SecondCountChanged -= OnSecondBadgeCountChanged;
            }
        }
    }
}
