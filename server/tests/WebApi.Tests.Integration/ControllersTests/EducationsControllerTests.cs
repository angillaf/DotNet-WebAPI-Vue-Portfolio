﻿using Application.DTOs;
using System.Net.Http.Json;
using FluentAssertions;

namespace WebApi.Tests.Integration.ControllersTests;

public class EducationsControllerTests : BaseControllerTest
{
    public EducationsControllerTests(IntegrationTestWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task GetAll_Returns_SuccessStatusCode()
    {
        // Arrange
        // Act
        var response = await _httpClient.GetAsync("api/educations");

        // Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetById_Returns_SuccessStatusCode()
    {
        // Arrange
        await AuthenticateAsync();
        Guid id = Guid.NewGuid();

        // Act
        var response = await _httpClient.GetAsync("api/educations/" + id);

        // Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Post_Returns_OK_With_EducationDto()
    {
        // Arrange
        await AuthenticateAsync();
        EducationDto dto = new EducationDto
        {
            Degree = "Master's degree",
            FieldOfStudy = "Artificial Intelligence",
            StartYear = "2020",
            EndYear = "2022",
            School = "Solstice College"
        };

        // Act
        var response = await _httpClient.PostAsJsonAsync("/api/educations", dto);

        // Assert
        response.EnsureSuccessStatusCode();

        var responseDto = await response.Content.ReadFromJsonAsync<EducationDto>();
        dto.Should().BeEquivalentTo(responseDto, options => options.Excluding(x => x.Id));
    }
}
