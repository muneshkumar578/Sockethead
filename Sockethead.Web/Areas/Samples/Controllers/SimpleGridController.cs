﻿using Microsoft.AspNetCore.Mvc;
using Sockethead.Web.Data;
using Sockethead.Web.Data.Entities;
using System.Linq;
using Sockethead.Razor.Helpers;
using Sockethead.Web.Areas.Samples.ViewModels;
using System.Collections.Generic;

namespace Sockethead.Web.Areas.Samples.Controllers
{
    [Area("Samples")]
    public class SimpleGridController : Controller
    {
        private static List<SocketheadRazorFeature> Features { get; } = new List<SocketheadRazorFeature>
        {
            new SocketheadRazorFeature
            {
                Name = "Installation",
                Description = "Just install the NuGet package and go!",
            },
            new SocketheadRazorFeature
            {
                Name = "Basic Usage",
                Description = "A minimial SimpleGrid.  This includes the Movie model for reference which is used in many of the other Samples as well.",
            },
            new SocketheadRazorFeature
            {
                Name = "Kitchen Sink",
                Description = "A sample SimpleGrid using many of the options available.",
            },
            new SocketheadRazorFeature
            {
                Name = "Column Selection",
                Description = "There are several ways to control which columns to include in your grid.",
            },
            new SocketheadRazorFeature
            {
                Name = "Column Headers",
                Description = "How to specify what text to render for column headers.",
            },
            new SocketheadRazorFeature
            {
                Name = "No Headers",
                Description = "Surpress column headers altogether.",
            },
            new SocketheadRazorFeature
            {
                Name = "Column Display",
                Description = "How to build the item render.",
            },
            new SocketheadRazorFeature
            {
                Name = "Links",
                Description = "Create links in item render",
            },
            new SocketheadRazorFeature
            {
                Name = "Bullet List",
                Description = "Render an HTML bullet list",
            },
            new SocketheadRazorFeature
            {
                Name = "Enum",
                Description = "Render an enumerated value as an int, field name, or Display Name.",
            },
            new SocketheadRazorFeature
            {
                Name = "Embedding Grids",
                Description = "Embed TwoColumnGrids and SimpleGrids inside your grid.  How meta!",
            },
            new SocketheadRazorFeature
            {
                Name = "Sorting",
                Description = "Enable dynamic sorting on based on columns and specify the default sort order.",
            },
            new SocketheadRazorFeature
            {
                Name = "Pagination",
                Description = "Add a Pager to the grid for large data sets.",
            },
            new SocketheadRazorFeature
            {
                Name = "Search",
                Description = "Add custom search capabilities quickly and easily.",
            },
            new SocketheadRazorFeature
            {
                Name = "CSS",
                Description = "Apply CSS classes and styles to the table, header, and rows.",
            },
            new SocketheadRazorFeature
            {
                Name = "Row Modifier",
                Description = "Apply CSS on a row based on a criteria.",
            },
            new SocketheadRazorFeature
            {
                Name = "Options",
                Description = "Set some grid options such as maximum rows and the No records message.  Also override partial view with your own.",
            },
            new SocketheadRazorFeature
            {
                Name = "AJAX",
                Description = "Use AJAX for paging and search in the grid.",
            },
            new SocketheadRazorFeature
            {
                Name = "Forms",
                Description = "Use a SimpleGrid as form.  This demonstrates checkbox handling.",
            },
            new SocketheadRazorFeature
            {
                Name = "Extensions",
                Description = "Create your own custom SimpleGrid extension!",
            },

            new SocketheadRazorFeature
            {
                Name = "Sample1",
                Model = "SampleData",
                Description = "Placeholder",
            },
            new SocketheadRazorFeature
            {
                Name = "Sample2",
                Model = "SampleData",
                Description = "Placeholder",
            },
            new SocketheadRazorFeature
            {
                Name = "Playground",
                Description = "A sample for me to play around in...",
            },

        };
    
    private static IQueryable<SampleModel> SampleDataQuery => SampleData.SampleModels.AsQueryable();
        private static IQueryable<Movie> MovieQuery => SampleData.Movies.AsQueryable();

        [HttpGet]
        public IActionResult Index() => RedirectToAction(actionName: nameof(Dashboard));

        [HttpGet]
        public IActionResult Dashboard() => View(Features.AsQueryable());

        [HttpGet]
        public IActionResult Sample(string name)
        {
            string modelName = "Movies";
            for (int i = 0; i < Features.Count; i++)
            {
                if (Features[i].Name == name)
                {
                    modelName = Features[i].Model;
                    if (i > 0) ViewData["PrevFeature"] = Features[i-1];
                    if (i+1 < Features.Count) ViewData["NextFeature"] = Features[i+1];
                    break;
                }
            }

            var model = modelName == "Movies" ? MovieQuery : (object)SampleDataQuery;

            return View(viewName: name.Replace(" ", ""), model: model).SetTitle(name);
        }

        [HttpGet]
        public PartialViewResult PartialGrid() => PartialView("_PartialGrid", MovieQuery);

        [HttpGet]
        public IActionResult Playground() => View(MovieQuery).SetTitle("Playground");

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Playground(string[] names) => View(MovieQuery.Where(movie => names.Contains(movie.Name))).SetTitle("Playground Submit Results");


        [HttpGet]
        public IActionResult Sample1()
        {
            ViewData["Title"] = "Sample1";
            return View(SampleDataQuery);
        }

        [HttpGet]
        public IActionResult Sample2()
        {
            ViewData["Title"] = "Sample2";
            return View(SampleDataQuery);
        }

    }
}
