using KeywordLinkMemo.Views;
using KeywordLinkMemo.ViewModels;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;

namespace KeywordLinkMemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<SelectMemoGroupWindow>();
            containerRegistry.RegisterDialog<CreateMemoGroupWindow>();
            containerRegistry.RegisterDialog<DeleteMemoGroupWindow>();

            containerRegistry.RegisterForNavigation<BlankPage>();
            containerRegistry.RegisterForNavigation<ShowMemoItemPage>();
            containerRegistry.RegisterForNavigation<EditMemoItemPage>();
        }
    }
}
