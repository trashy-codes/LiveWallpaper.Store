using EasyMvvm;
using LiveWallpaper.Server;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveWallpaper.Store.ViewModels
{
    public class WallpapersViewModel : EasyViewModel
    {
        int _pageIndex = 0;
        LocalServer _localServer;
        public WallpapersViewModel(LocalServer server)
        {
            _localServer = server;
            _localServer.UnLock("whosyourdady");

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
                NotifyOfPropertyChange(SelectedWallpaperPropertyName);
            }
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
