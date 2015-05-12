﻿using ServerJavaConnector.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ServerJavaConnector.XAML.Pages
{
    /// <summary>
    /// Manages pages in frames contained in window
    /// </summary>
    public class PageManager
    {
        private Dictionary<FrameType, Frame> _frames;
        private Dictionary<PageType, Page> _pages;

        public PageManager(Dictionary<FrameType, Frame> frames)
        {
            instance = this;
            this.Frames = frames;
            this.Pages = getPages();
        }

        private static Dictionary<PageType, Page> getPages()
        {
            Dictionary<PageType, Page> pages = new Dictionary<PageType, Page>();
            pages.Add(PageType.MainPage, new MainPage());
            pages.Add(PageType.LoginPage, new LoginPage());
            //TODO: pages.Add(PageType.RegisterPage, new RegisterPage()); 
            return pages;
        }

        /// <summary>
        /// Initial startup setup of frames
        /// </summary>
        public void initSetup()
        {
            getFrame(FrameType.MainFrame).Navigate(getPage(PageType.MainPage));
        }

        /// <summary>
        /// Changes page in specified frame to specified page.
        /// </summary>
        /// <param name="fT"></param>
        /// <param name="pT"></param>
        /// <returns>True if successfully changed. False when page is opened in different frame.</returns>
        public bool changePage(FrameType fT, PageType pT)
        {
            Frame frame=getFrame(fT);
            Page page=getPage(pT);
            foreach (KeyValuePair<FrameType, Frame> entry in Frames)
            {
                if (entry.Value.Content == page)
                {
                    return false;
                }
            }
            frame.Navigate(page);
            return true;
        }

        private Page getPage(PageType pT){
            Page page;
            Pages.TryGetValue(pT, out page);
            return page;
        }

        private Frame getFrame(FrameType fT)
        {
            Frame frame;
            Frames.TryGetValue(fT, out frame);
            return frame;
        }

        public static PageManager instance { get; private set; }

        public Dictionary<FrameType, Frame> Frames
        {
            get { return _frames; }
            private set { _frames = value; }
        }

        public Dictionary<PageType, Page> Pages
        {
            get { return _pages; }
            private set { this._pages = value; }
        }
    }

    public enum PageType
    {
        MainPage, LoginPage, RegisterPage
    }

    public enum FrameType
    {
        MainFrame, TopFrame, BottomFrame
    }
}
