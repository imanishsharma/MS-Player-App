﻿

#pragma checksum "C:\Users\Mannu.manish\Documents\Visual Studio 2012\Projects\MS Player App\MS Player App\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "23581824719D72A60EB531B917544F8D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MS_Player_App
{
    partial class MainPage : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 48 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.PlayButton_Click_1;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 49 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.PauseButton_Click_1;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 50 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.PreviousButton_Click_1;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 51 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.NextButton_Click_1;
                 #line default
                 #line hidden
                break;
            case 5:
                #line 52 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.ToggleSwitch)(target)).Toggled += this.ToggleSwitchRepeat_Toggled_1;
                 #line default
                 #line hidden
                break;
            case 6:
                #line 54 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.RangeBase)(target)).ValueChanged += this.SliderVolume_ValueChanged_1;
                 #line default
                 #line hidden
                break;
            case 7:
                #line 55 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.RangeBase)(target)).ValueChanged += this.SongPosition_ValueChanged_1;
                 #line default
                 #line hidden
                break;
            case 8:
                #line 56 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.fullScreenBtn_Click_1;
                 #line default
                 #line hidden
                break;
            case 9:
                #line 57 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.fullScreenBtn_Click_1;
                 #line default
                 #line hidden
                break;
            case 10:
                #line 24 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).RightTapped += this.PlaylistView_RightTapped_1;
                 #line default
                 #line hidden
                #line 24 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.Selector)(target)).SelectionChanged += this.PlaylistView_SelectionChanged_1;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}

