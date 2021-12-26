using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using VeganMap.Models;

namespace VeganMap.Areas.Admin.Pages;

public class PlaceModel : PageModel
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IConfiguration _configuration;

    [BindProperty]
    public Place Place { get; set; }
    public SelectList NoYesOptions { get; private set; }
    public SelectList CitySelectList { get; private set; }
    public SelectList TypeSelectList { get; private set; }
    public SelectList WorkHoursSelectList { get; private set; }
    public SelectList WorkMinutesSelectList { get; private set; }
    public bool IsCreate => !Place?.IsSaved ?? true;
    public bool IsEdit => !IsCreate;
    [TempData]
    public bool HasSuccessMessage { get; set; }
    public bool HasErrorMessage { get; set; }

    [BindProperty]
    public int FromHourMonday { get; set; }
    [BindProperty]
    public int FromMinutesMonday { get; set; }
    [BindProperty]
    public int ToHourMonday { get; set; }
    [BindProperty]
    public int ToMinutesMonday { get; set; }

    [BindProperty]
    public int FromHourTuesday { get; set; }
    [BindProperty]
    public int FromMinutesTuesday { get; set; }
    [BindProperty]
    public int ToHourTuesday { get; set; }
    [BindProperty]
    public int ToMinutesTuesday { get; set; }

    [BindProperty]
    public int FromHourWednesday { get; set; }
    [BindProperty]
    public int FromMinutesWednesday { get; set; }
    [BindProperty]
    public int ToHourWednesday { get; set; }
    [BindProperty]
    public int ToMinutesWednesday { get; set; }

    [BindProperty]
    public int FromHourThursday { get; set; }
    [BindProperty]
    public int FromMinutesThursday { get; set; }
    [BindProperty]
    public int ToHourThursday { get; set; }
    [BindProperty]
    public int ToMinutesThursday { get; set; }

    [BindProperty]
    public int FromHourFriday { get; set; }
    [BindProperty]
    public int FromMinutesFriday { get; set; }
    [BindProperty]
    public int ToHourFriday { get; set; }
    [BindProperty]
    public int ToMinutesFriday { get; set; }

    [BindProperty]
    public int FromHourSaturday { get; set; }
    [BindProperty]
    public int FromMinutesSaturday { get; set; }
    [BindProperty]
    public int ToHourSaturday { get; set; }
    [BindProperty]
    public int ToMinutesSaturday { get; set; }

    [BindProperty]
    public int FromHourSunday { get; set; }
    [BindProperty]
    public int FromMinutesSunday { get; set; }
    [BindProperty]
    public int ToHourSunday { get; set; }
    [BindProperty]
    public int ToMinutesSunday { get; set; }

    public IList<IFormFile> FormFiles { get; set; }

    public PlaceModel(ApplicationDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        await InitSelectLists();

        if (id == null)
        {
            Place = new Place();
        }
        else
        {
            var place = await _dbContext.Places
                .Include(x => x.WorkHours)
                .Include(x => x.Pictures)
                .FirstOrDefaultAsync(x => x.Id == id);
            Place = place;
        }

        InitWorkHours();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await InitSelectLists();
        ModelState.Remove("Place.City");

        if (!ModelState.IsValid)
        {
            HasErrorMessage = true;
            return Page();
        }

        Place.City = await _dbContext.Cities.FindAsync(Place.CityId);
        if (!Place.IsSaved)
        {
            Place.CreatedOn = DateTime.Now;
            _dbContext.Places.Add(Place);
        }
        else
        {
            _dbContext.Places.Update(Place);
        }

        HasSuccessMessage = true;
        await _dbContext.SaveChangesAsync();

        return RedirectToPage("Place", new { id = Place.Id });
    }

    public async Task<IActionResult> OnPostWorkHoursAsync()
    {
        var place = await _dbContext.Places.Include(x => x.WorkHours).FirstOrDefaultAsync(x => x.Id == Place.Id);
        if (place == null)
        {
            return RedirectToPage("PlaceList");
        }

        { // Monday
            var monday = place.WorkHours.FirstOrDefault(x => x.DayOfWeek == DayOfWeek.Monday);
            if (monday == null)
            {
                monday = new WorkingHour
                {
                    CreatedOn = DateTime.Now,
                    DayOfWeek = DayOfWeek.Monday,
                    FromHour = FromHourMonday,
                    FromMinute = FromMinutesMonday,
                    ToHour = ToHourMonday,
                    ToMinute = ToMinutesMonday,
                    Place = place,
                };
                place.WorkHours.Add(monday);
            }
            else
            {
                monday.FromHour = FromHourMonday;
                monday.FromMinute = FromMinutesMonday;
                monday.ToHour = ToHourMonday;
                monday.ToMinute = ToMinutesMonday;
            }
        }

        { // Tuesday
            var tuesday = place.WorkHours.FirstOrDefault(x => x.DayOfWeek == DayOfWeek.Tuesday);
            if (tuesday == null)
            {
                tuesday = new WorkingHour
                {
                    CreatedOn = DateTime.Now,
                    DayOfWeek = DayOfWeek.Tuesday,
                    FromHour = FromHourTuesday,
                    FromMinute = FromMinutesTuesday,
                    ToHour = ToHourTuesday,
                    ToMinute = ToMinutesTuesday,
                    Place = place,
                };
                place.WorkHours.Add(tuesday);
            }
            else
            {
                tuesday.FromHour = FromHourTuesday;
                tuesday.FromMinute = FromMinutesTuesday;
                tuesday.ToHour = ToHourTuesday;
                tuesday.ToMinute = ToMinutesTuesday;
            }
        }

        { // Wednesday
            var wednesday = place.WorkHours.FirstOrDefault(x => x.DayOfWeek == DayOfWeek.Wednesday);
            if (wednesday == null)
            {
                wednesday = new WorkingHour
                {
                    CreatedOn = DateTime.Now,
                    DayOfWeek = DayOfWeek.Wednesday,
                    FromHour = FromHourWednesday,
                    FromMinute = FromMinutesWednesday,
                    ToHour = ToHourWednesday,
                    ToMinute = ToMinutesWednesday,
                    Place = place,
                };
                place.WorkHours.Add(wednesday);
            }
            else
            {
                wednesday.FromHour = FromHourWednesday;
                wednesday.FromMinute = FromMinutesWednesday;
                wednesday.ToHour = ToHourWednesday;
                wednesday.ToMinute = ToMinutesWednesday;
            }
        }

        { // Thursday
            var thursday = place.WorkHours.FirstOrDefault(x => x.DayOfWeek == DayOfWeek.Thursday);
            if (thursday == null)
            {
                thursday = new WorkingHour
                {
                    CreatedOn = DateTime.Now,
                    DayOfWeek = DayOfWeek.Thursday,
                    FromHour = FromHourThursday,
                    FromMinute = FromMinutesThursday,
                    ToHour = ToHourThursday,
                    ToMinute = ToMinutesThursday,
                    Place = place,
                };
                place.WorkHours.Add(thursday);
            }
            else
            {
                thursday.FromHour = FromHourThursday;
                thursday.FromMinute = FromMinutesThursday;
                thursday.ToHour = ToHourThursday;
                thursday.ToMinute = ToMinutesThursday;
            }
        }

        { // Friday
            var friday = place.WorkHours.FirstOrDefault(x => x.DayOfWeek == DayOfWeek.Friday);
            if (friday == null)
            {
                friday = new WorkingHour
                {
                    CreatedOn = DateTime.Now,
                    DayOfWeek = DayOfWeek.Friday,
                    FromHour = FromHourFriday,
                    FromMinute = FromMinutesFriday,
                    ToHour = ToHourFriday,
                    ToMinute = ToMinutesFriday,
                    Place = place,
                };
                place.WorkHours.Add(friday);
            }
            else
            {
                friday.FromHour = FromHourFriday;
                friday.FromMinute = FromMinutesFriday;
                friday.ToHour = ToHourFriday;
                friday.ToMinute = ToMinutesFriday;
            }
        }

        { // Saturday
            var saturday = place.WorkHours.FirstOrDefault(x => x.DayOfWeek == DayOfWeek.Saturday);
            if (saturday == null)
            {
                saturday = new WorkingHour
                {
                    CreatedOn = DateTime.Now,
                    DayOfWeek = DayOfWeek.Saturday,
                    FromHour = FromHourSaturday,
                    FromMinute = FromMinutesSaturday,
                    ToHour = ToHourSaturday,
                    ToMinute = ToMinutesSaturday,
                    Place = place,
                };
                place.WorkHours.Add(saturday);
            }
            else
            {
                saturday.FromHour = FromHourSaturday;
                saturday.FromMinute = FromMinutesSaturday;
                saturday.ToHour = ToHourSaturday;
                saturday.ToMinute = ToMinutesSaturday;
            }
        }

        { // Sunday
            var sunday = place.WorkHours.FirstOrDefault(x => x.DayOfWeek == DayOfWeek.Sunday);
            if (sunday == null)
            {
                sunday = new WorkingHour
                {
                    CreatedOn = DateTime.Now,
                    DayOfWeek = DayOfWeek.Sunday,
                    FromHour = FromHourSunday,
                    FromMinute = FromMinutesSunday,
                    ToHour = ToHourSunday,
                    ToMinute = ToMinutesSunday,
                    Place = place,
                };
                place.WorkHours.Add(sunday);
            }
            else
            {
                sunday.FromHour = FromHourSunday;
                sunday.FromMinute = FromMinutesSunday;
                sunday.ToHour = ToHourSunday;
                sunday.ToMinute = ToMinutesSunday;
            }
        }

        HasSuccessMessage = true;
        await _dbContext.SaveChangesAsync();

        return RedirectToPage("Place", new { id = Place.Id });
    }

    public async Task<IActionResult> OnPostPictureAsync()
    {
        var place = await _dbContext.Places
            .Include(x => x.WorkHours)
            .Include(x => x.Pictures)
            .FirstOrDefaultAsync(x => x.Id == Place.Id);
        if (place == null)
        {
            return RedirectToPage("PlaceList");
        }

        var pathToPlaceImages = Path.Combine(_configuration["AppSettings:PathToPlaceImages"], place.Id.ToString());
        if (!Directory.Exists(pathToPlaceImages))
        {
            Directory.CreateDirectory(pathToPlaceImages);
        }

        var fullSizeWidth = int.Parse(_configuration["AppSettings:PlaceImageFullSizeWidth"]);
        var fullSizeHeight = int.Parse(_configuration["AppSettings:PlaceImageFullSizeHeight"]);
        var thumbWidth = int.Parse(_configuration["AppSettings:PlaceImageThumbWidth"]);
        var thumbHeight= int.Parse(_configuration["AppSettings:PlaceImageThumbHeight"]);

        foreach (var formFile in FormFiles)
        {
            if (formFile.Length > 0)
            {
                var ext = Path.GetExtension(formFile.FileName);
                var newFileNameFullSize = Path.GetRandomFileName().Replace(".", string.Empty) + ext;
                var newFileNameThumb = Path.GetRandomFileName().Replace(".", string.Empty) + ext;
                var fullSizeFilePath = Path.Combine(pathToPlaceImages, newFileNameFullSize);
                var thumbFilePath = Path.Combine(pathToPlaceImages, newFileNameThumb);

                using (Image image = Image.Load(formFile.OpenReadStream()))
                {
                    image.Mutate(x => x.Resize(fullSizeWidth, fullSizeHeight));
                    image.Save(fullSizeFilePath);

                    image.Mutate(x => x.Resize(thumbWidth, thumbHeight));
                    image.Save(thumbFilePath);
                }

                var placePicture = new PlacePicture
                {
                    Place = place,
                    FullSizeFilename = newFileNameFullSize,
                    FullSizeWidth = fullSizeWidth,
                    FullSizeHeight = fullSizeHeight,
                    ThumbFilename = newFileNameThumb,
                    ThumbWidth = thumbWidth,
                    ThumbHeight = thumbHeight,
                    CreatedOn = DateTime.Now,
                    Sort = place.Pictures.Count + 1,
                };
                place.Pictures.Add(placePicture);
            }
        }

        await _dbContext.SaveChangesAsync();

        return RedirectToPage("Place", new { id = Place.Id });
    }

    public async Task<IActionResult> OnGetDeletePictureAsync(int pictureId)
    {
        var picture = await _dbContext.PlacePictures.Include(x => x.Place).FirstOrDefaultAsync(x => x.Id == pictureId);
        if (picture == null)
        {
            return RedirectToPage("PlaceList");
        }

        picture.DeletedOn = DateTime.Now;
        picture.IsDeleted = true;

        HasSuccessMessage = true;
        await _dbContext.SaveChangesAsync();

        return RedirectToPage("Place", new { id = picture.Place.Id });
    }

    private async Task InitSelectLists()
    {
        NoYesOptions = new SelectList(new SelectListItem[] { new SelectListItem("Не", "False"), new SelectListItem("Да", "True") }, nameof(SelectListItem.Value), nameof(SelectListItem.Text));
        CitySelectList = new SelectList(await _dbContext.Cities.AsNoTracking().ToListAsync(), nameof(City.Id), nameof(City.Name));
        TypeSelectList = new SelectList(Enum.GetValues(typeof(PlaceTypeEnum)).Cast<PlaceTypeEnum>().Select(x => new SelectListItem(x.ToString(), ((int)x).ToString())), nameof(SelectListItem.Value), nameof(SelectListItem.Text));
        WorkHoursSelectList = new SelectList(Enumerable.Range(0, 24).Select(x => new SelectListItem(x.ToString("d2"), x.ToString())).Prepend(new SelectListItem("--", "-1")), nameof(SelectListItem.Value), nameof(SelectListItem.Text));
        WorkMinutesSelectList = new SelectList(new int[] { 0, 15, 30, 45 }.Select(x => new SelectListItem(x.ToString("d2"), x.ToString())).Prepend(new SelectListItem("--", "-1")), nameof(SelectListItem.Value), nameof(SelectListItem.Text));
    }

    private void InitWorkHours()
    {
        {
            var monday = Place.WorkHours.FirstOrDefault(x => x.DayOfWeek == DayOfWeek.Monday);
            FromHourMonday = monday?.FromHour ?? -1;
            FromMinutesMonday = monday?.FromMinute ?? -1;
            ToHourMonday = monday?.ToHour ?? -1;
            ToMinutesMonday = monday?.ToMinute ?? -1;
        }

        {
            var tuesday = Place.WorkHours.FirstOrDefault(x => x.DayOfWeek == DayOfWeek.Tuesday);
            FromHourTuesday = tuesday?.FromHour ?? -1;
            FromMinutesTuesday = tuesday?.FromMinute ?? -1;
            ToHourTuesday = tuesday?.ToHour ?? -1;
            ToMinutesTuesday = tuesday?.ToMinute ?? -1;
        }

        {
            var wednesday = Place.WorkHours.FirstOrDefault(x => x.DayOfWeek == DayOfWeek.Wednesday);
            FromHourWednesday = wednesday?.FromHour ?? -1;
            FromMinutesWednesday = wednesday?.FromMinute ?? -1;
            ToHourWednesday = wednesday?.ToHour ?? -1;
            ToMinutesWednesday = wednesday?.ToMinute ?? -1;
        }

        {
            var thursday = Place.WorkHours.FirstOrDefault(x => x.DayOfWeek == DayOfWeek.Thursday);
            FromHourThursday = thursday?.FromHour ?? -1;
            FromMinutesThursday = thursday?.FromMinute ?? -1;
            ToHourThursday = thursday?.ToHour ?? -1;
            ToMinutesThursday = thursday?.ToMinute ?? -1;
        }

        {
            var friday = Place.WorkHours.FirstOrDefault(x => x.DayOfWeek == DayOfWeek.Friday);
            FromHourFriday = friday?.FromHour ?? -1;
            FromMinutesFriday = friday?.FromMinute ?? -1;
            ToHourFriday = friday?.ToHour ?? -1;
            ToMinutesFriday = friday?.ToMinute ?? -1;
        }

        {
            var saturday = Place.WorkHours.FirstOrDefault(x => x.DayOfWeek == DayOfWeek.Saturday);
            FromHourSaturday = saturday?.FromHour ?? -1;
            FromMinutesSaturday = saturday?.FromMinute ?? -1;
            ToHourSaturday = saturday?.ToHour ?? -1;
            ToMinutesSaturday = saturday?.ToMinute ?? -1;
        }

        {
            var sunday = Place.WorkHours.FirstOrDefault(x => x.DayOfWeek == DayOfWeek.Sunday);
            FromHourSunday = sunday?.FromHour ?? -1;
            FromMinutesSunday = sunday?.FromMinute ?? -1;
            ToHourSunday = sunday?.ToHour ?? -1;
            ToMinutesSunday = sunday?.ToMinute ?? -1;
        }
    }
}
