﻿using App.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace WebApplication_G.Controllers;

[Authorize]
public class CoursesController(HttpClient httpClient) : Controller
{
    private readonly HttpClient _httpClient = httpClient;

    [Route("/courses")]
    public async Task<IActionResult> Index()
    {
        var viewModel = new CourseIndexViewModel();

        var response = await _httpClient.GetAsync("https://localhost:7293/api/courses");
        if (response.IsSuccessStatusCode)
        {
            var courses = JsonConvert.DeserializeObject<IEnumerable<CourseViewModel>>(await response.Content.ReadAsStringAsync());
            if (courses != null && courses.Any())
                viewModel.Courses = courses;
        }

        return View(viewModel);
    }
}
