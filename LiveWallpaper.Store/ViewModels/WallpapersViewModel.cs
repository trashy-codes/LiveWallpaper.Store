using EasyMvvm;
using LiveWallpaper.Server;
using Mvvm.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace LiveWallpaper.Store.ViewModels
{
    public class WallpapersViewModel : EasyViewModel
    {
        int _pageIndex = 0;
        LocalServer _localServer;
        public WallpapersViewModel(LocalServer server)
        {
            SingletonView = true;
            _localServer = server;
            _localServer.UnLock("whosyourdady");
        }

        public override void OnViewLoaded()
        {
            base.OnViewLoaded();
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            LoadTagsAndSorts();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

        }

        #region  properties

        #region IsBusy

        /// <summary>
        /// The <see cref="IsBusy" /> property's name.
        /// </summary>
        public const string IsBusyPropertyName = "IsBusy";

        private bool _IsBusy;

        /// <summary>
        /// IsBusy
        /// </summary>
        public bool IsBusy
        {
            get { return _IsBusy; }

            set
            {
                if (_IsBusy == value) return;

                _IsBusy = value;
                NotifyOfPropertyChange(IsBusyPropertyName);
            }
        }

        #endregion

        #region Tags

        /// <summary>
        /// The <see cref="Tags" /> property's name.
        /// </summary>
        public const string TagsPropertyName = "Tags";

        private ObservableCollection<TagServerObj> _Tags;

        /// <summary>
        /// Tags
        /// </summary>
        public ObservableCollection<TagServerObj> Tags
        {
            get { return _Tags; }

            set
            {
                if (_Tags == value) return;

                _Tags = value;
                NotifyOfPropertyChange(TagsPropertyName);
            }
        }

        #endregion

        #region SelectedTag

        /// <summary>
        /// The <see cref="SelectedTag" /> property's name.
        /// </summary>
        public const string SelectedTagPropertyName = "SelectedTag";

        private TagServerObj _SelectedTag;

        /// <summary>
        /// SelectedTag
        /// </summary>
        public TagServerObj SelectedTag
        {
            get { return _SelectedTag; }

            set
            {
                if (_SelectedTag == value) return;

                _SelectedTag = value;
                ReLoadWallpapers();
                NotifyOfPropertyChange(SelectedTagPropertyName);
            }
        }

        #endregion

        #region Sorts

        /// <summary>
        /// The <see cref="Sorts" /> property's name.
        /// </summary>
        public const string SortsPropertyName = "Sorts";

        private ObservableCollection<SortServerObj> _Sorts;

        /// <summary>
        /// Sorts
        /// </summary>
        public ObservableCollection<SortServerObj> Sorts
        {
            get { return _Sorts; }

            set
            {
                if (_Sorts == value) return;

                _Sorts = value;
                NotifyOfPropertyChange(SortsPropertyName);
            }
        }

        #endregion

        #region SelectedSort

        /// <summary>
        /// The <see cref="SelectedSort" /> property's name.
        /// </summary>
        public const string SelectedSortPropertyName = "SelectedSort";

        private SortServerObj _SelectedSort;

        /// <summary>
        /// SelectedSort
        /// </summary>
        public SortServerObj SelectedSort
        {
            get { return _SelectedSort; }

            set
            {
                if (_SelectedSort == value) return;

                _SelectedSort = value;
                ReLoadWallpapers();
                NotifyOfPropertyChange(SelectedSortPropertyName);
            }
        }


        #endregion

        #region Wallpapers

        /// <summary>
        /// The <see cref="Wallpapers" /> property's name.
        /// </summary>
        public const string WallpapersPropertyName = "Wallpapers";

        private ObservableCollection<WallpaperServerObj> _Wallpapers;

        /// <summary>
        /// Wallpapers
        /// </summary>
        public ObservableCollection<WallpaperServerObj> Wallpapers
        {
            get { return _Wallpapers; }

            set
            {
                if (_Wallpapers == value) return;

                _Wallpapers = value;
                NotifyOfPropertyChange(WallpapersPropertyName);
            }
        }

        #endregion

        #region SelectedWallpaper

        /// <summary>
        /// The <see cref="SelectedWallpaper" /> property's name.
        /// </summary>
        public const string SelectedWallpaperPropertyName = "SelectedWallpaper";

        private WallpaperServerObj _SelectedWallpaper;

        /// <summary>
        /// SelectedWallpaper
        /// </summary>
        public WallpaperServerObj SelectedWallpaper
        {
            get { return _SelectedWallpaper; }

            set
            {
                if (_SelectedWallpaper == value) return;

                _SelectedWallpaper = value;
                CanDownload = value != null;
                DownloadCommand.RaiseCanExecuteChanged();
                NotifyOfPropertyChange(SelectedWallpaperPropertyName);
            }
        }

        #endregion

        #region CanDownload

        /// <summary>
        /// The <see cref="CanDownload" /> property's name.
        /// </summary>
        public const string CanDownloadPropertyName = "CanDownload";

        private bool _CanDownload;

        /// <summary>
        /// CanDownload
        /// </summary>
        public bool CanDownload
        {
            get { return _CanDownload; }

            set
            {
                if (_CanDownload == value) return;

                _CanDownload = value;
                NotifyOfPropertyChange(CanDownloadPropertyName);
            }
        }

        #endregion

        #region Downloading

        /// <summary>
        /// The <see cref="Downloading" /> property's name.
        /// </summary>
        public const string DownloadingPropertyName = "Downloading";

        private bool _Downloading;

        /// <summary>
        /// Downloading
        /// </summary>
        public bool Downloading
        {
            get { return _Downloading; }

            set
            {
                if (_Downloading == value) return;

                _Downloading = value;
                NotifyOfPropertyChange(DownloadingPropertyName);
            }
        }

        #endregion

        #region BytesReceived

        /// <summary>
        /// The <see cref="BytesReceived" /> property's name.
        /// </summary>
        public const string BytesReceivedPropertyName = "BytesReceived";

        private double _BytesReceived;

        /// <summary>
        /// BytesReceived
        /// </summary>
        public double BytesReceived
        {
            get { return _BytesReceived; }

            set
            {
                if (_BytesReceived == value) return;

                _BytesReceived = value;
                NotifyOfPropertyChange(BytesReceivedPropertyName);
            }
        }

        #endregion

        #region TotalBytesToReceive

        /// <summary>
        /// The <see cref="TotalBytesToReceive" /> property's name.
        /// </summary>
        public const string TotalBytesToReceivePropertyName = "TotalBytesToReceive";

        private double _TotalBytesToReceive;

        /// <summary>
        /// TotalBytesToReceive
        /// </summary>
        public double TotalBytesToReceive
        {
            get { return _TotalBytesToReceive; }

            set
            {
                if (_TotalBytesToReceive == value) return;

                _TotalBytesToReceive = value;
                NotifyOfPropertyChange(TotalBytesToReceivePropertyName);
            }
        }

        #endregion

        #endregion

        #region Commands

        #region DownloadCommand 

        private DelegateCommand _DownloadCommand;

        /// <summary>
        /// Gets the DownloadCommand.
        /// </summary>
        public DelegateCommand DownloadCommand
        {
            get
            {
                return _DownloadCommand ?? (_DownloadCommand = new DelegateCommand(ExecuteDownloadCommand, CanExecuteDownloadCommand));
            }
        }

        private async void ExecuteDownloadCommand()
        {
            var selected = SelectedWallpaper;
            if (Downloading || selected == null)
                return;

            try
            {
                Downloading = true;
                WebClient client = new WebClient();
                string saveDir = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
                saveDir = Path.Combine(saveDir, "LivewallpaperCache", Guid.NewGuid().ToString());
                Directory.CreateDirectory(saveDir);

                string previewPath = $"preview{ Path.GetExtension(selected.Img)}";
                string videoPath = $"index{ Path.GetExtension(selected.URL)}";

                //json
                ProjectInfo info = new ProjectInfo
                {
                    Title = selected.Name,
                    Type = WallpaperType.Video.ToString(),
                    Preview = previewPath,
                    File = videoPath
                };
                var json = JsonConvert.SerializeObject(info);
                var projectFile = Path.Combine(saveDir, "project.json");

                await WriteTextAsync(projectFile, json);

                previewPath = Path.Combine(saveDir, previewPath);
                videoPath = Path.Combine(saveDir, videoPath);

                //图片
                await client.DownloadFileTaskAsync(new Uri(selected.Img), previewPath);

                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);

                //视频
                client.DownloadFileAsync(new Uri(selected.URL), videoPath);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        static async Task WriteTextAsync(string filePath, string text)
        {
            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                await writer.WriteAsync(text);
            }
        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            BytesReceived = e.BytesReceived;
            TotalBytesToReceive = e.TotalBytesToReceive;
            //System.Diagnostics.Debug.WriteLine(e.BytesReceived + "|" + e.TotalBytesToReceive);
        }
        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Downloading = false;
        }

        private bool CanExecuteDownloadCommand()
        {
            return SelectedWallpaper != null;
        }

        #endregion

        #region LoadMoreWallpapersCommand

        private DelegateCommand _LoadMoreWallpapersCommand;

        /// <summary>
        /// Gets the LoadMoreWallpapersCommand.
        /// </summary>
        public DelegateCommand LoadMoreWallpapersCommand
        {
            get
            {
                return _LoadMoreWallpapersCommand ?? (_LoadMoreWallpapersCommand = new DelegateCommand(ExecuteLoadMoreWallpapersCommand));
            }
        }

        private void ExecuteLoadMoreWallpapersCommand()
        {
            LoadWallpapers();
        }

        #endregion

        #endregion

        #region methods

        private void ReLoadWallpapers()
        {
            _pageIndex = 0;
            Wallpapers?.Clear();
            LoadWallpapers();
        }

        public async Task LoadTagsAndSorts()
        {
            var tempTag = await _localServer.GetTags();
            if (tempTag == null)
                return;
            Tags = new ObservableCollection<TagServerObj>(tempTag);

            if (Tags != null && Tags.Count > 0)
                SelectedTag = Tags[0];

            var tempSort = await _localServer.GetSorts();
            if (tempSort == null)
                return;

            Sorts = new ObservableCollection<SortServerObj>(tempSort);
            if (Sorts != null && Sorts.Count > 0)
                SelectedSort = Sorts[0];
            LoadWallpapers();
        }

        public async void LoadWallpapers()
        {
            if (SelectedTag == null || SelectedSort == null)
                return;

            if (Wallpapers == null)
                Wallpapers = new ObservableCollection<WallpaperServerObj>();

            IsBusy = true;
            var tempList = await _localServer.GetWallpapers(SelectedTag.ID, SelectedSort.ID, _pageIndex++);
            IsBusy = false;

            if (tempList == null)
                return;

            tempList.ForEach(m => Wallpapers.Add(m));
        }

        #endregion
    }
}
