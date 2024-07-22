using System;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform.Compatibility;
using Microsoft.Maui.Platform;
using UIKit;

namespace Oportia
{
    public class TabbarBadgeRenderer : ShellRenderer
    {
        protected override IShellTabBarAppearanceTracker CreateTabBarAppearanceTracker()
        {
            return new BadgeShellTabbarAppearanceTracker();
        }
    }

    class BadgeShellTabbarAppearanceTracker : ShellTabBarAppearanceTracker
    {
        private UITabBarItem? _firstTabbarItem;
        private UITabBarItem? _secondTabbarItem;

        public override void UpdateLayout(UITabBarController controller)
        {
            base.UpdateLayout(controller);

            if (_firstTabbarItem is null || _secondTabbarItem is null)
            {
                const int firstTabbarItemIndex = 1;
                const int secondTabbarItemIndex = 2;

                _firstTabbarItem = controller.TabBar.Items?[firstTabbarItemIndex];
                _secondTabbarItem = controller.TabBar.Items?[secondTabbarItemIndex];

                if (_firstTabbarItem is not null && _secondTabbarItem is not null)
                {
                    UpdateFirstBadge(0);
                    UpdateSecondBadge(0);

                    BadgeCounterService.CountChanged += OnFirstBadgeCountChanged;
                    BadgeCounterService.SecondCountChanged += OnSecondBadgeCountChanged;
                }
            }

            UpdateTabIcons(controller);
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
            if (_firstTabbarItem is not null)
            {
                _firstTabbarItem.BadgeValue = count > 0 ? count.ToString() : null;
                _firstTabbarItem.BadgeColor = Colors.Red.ToPlatform();
            }
        }

        private void UpdateSecondBadge(int count)
        {
            if (_secondTabbarItem is not null)
            {
                _secondTabbarItem.BadgeValue = count > 0 ? count.ToString() : null;
                _secondTabbarItem.BadgeColor = Colors.Blue.ToPlatform(); // Change color if needed
            }
        }

        private void UpdateTabIcons(UITabBarController controller)
        {
            for (int i = 0; i < controller.TabBar.Items.Length; i++)
            {
                var item = controller.TabBar.Items[i];
                bool isSelected = controller.SelectedIndex == i;
                item.Image = ResizeImage(GetTabIcon(i, isSelected), 25, 25); // Unselected icon
                item.SelectedImage = ResizeImage(GetTabIcon(i, true), 25, 25); // Selected icon
            }
        }

        private UIImage GetTabIcon(int index, bool isSelected)
        {
            switch (index)
            {
                case 0:
                    return isSelected ? UIImage.FromBundle("icon_dashboard") : UIImage.FromBundle("icon_dashboard_black");
                case 1:
                    return isSelected ? UIImage.FromBundle("icon_chat") : UIImage.FromBundle("icon_chat_black");
                case 2:
                    return isSelected ? UIImage.FromBundle("icon_notification") : UIImage.FromBundle("icon_notification_black");
                case 3:
                    return isSelected ? UIImage.FromBundle("icon_leads") : UIImage.FromBundle("icon_leads_black");
                case 4:
                    return isSelected ? UIImage.FromBundle("icon_schedule") : UIImage.FromBundle("icon_schedule_black");
                default:
                    return UIImage.FromBundle("default_icon");
            }
        }

        private UIImage ResizeImage(UIImage sourceImage, float width, float height)
        {
            UIGraphics.BeginImageContext(new CoreGraphics.CGSize(width, height));
            sourceImage.Draw(new CoreGraphics.CGRect(0, 0, width, height));
            UIImage resultImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            return resultImage;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            BadgeCounterService.CountChanged -= OnFirstBadgeCountChanged;
            BadgeCounterService.SecondCountChanged -= OnSecondBadgeCountChanged;
        }
    }
}
