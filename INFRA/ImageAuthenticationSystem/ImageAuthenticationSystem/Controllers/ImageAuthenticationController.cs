// ImageAuthenticationController.cs
using ImageAuthenticationSystem.BAL;
using ImageAuthenticationSystem.DAL;
using ImageAuthenticationSystem.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")]
public class ImageAuthenticationController : ControllerBase
{
    #region "API"

    // Endpoint for user login
    [HttpPost("login")]
    public IActionResult Login([FromBody] ImageAuthenticationModel model)
    {
        // Check if the user is valid
        if (IsValidUser(model?.EmailId, model?.SelectedImageNames))
        {
            // Authentication successful
            DBConnection connection = new DBConnection();
            connection.StartConnection();
            return Ok(new { Message = "Authentication successful", UserName = model.EmailId });
        }
        else
        {
            // Authentication failed
            return Unauthorized(new { Message = "Authentication failed" });
        }
    }

    // Endpoint for creating a new user
    [HttpPost("newUser")]
    public IActionResult CreateNewUser([FromBody] ImageAuthenticationModel model)
    {
        string status;
        try
        {
            // User creation successful
            ImageBusinessLayer imageBusinessLayer = new ImageBusinessLayer();
            status = imageBusinessLayer.CreateUser(model);
            return Ok(new { Message = status, UserName = model.EmailId });
        }
        catch (Exception ex)
        {
            // Return unauthorized status and error message
            return Unauthorized(new { Message = ex.Message });
        }
    }

    // Endpoint for retrieving user password
    [HttpPost("getUserPassword")]
    public IActionResult GetUserPassword([FromBody] ImageAuthenticationModel model)
    {
        List<string>? password = null;
        try
        {
            // Retrieve user password successful
            ImageBusinessLayer imageBusinessLayer = new ImageBusinessLayer();
            password = new List<string>(imageBusinessLayer.GetUserPassword(model));
            return Ok(new { lstPassword = password });
        }
        catch (Exception ex)
        {
            // Return unauthorized status and error message
            return Unauthorized(new { Message = ex.Message });
        }
    }

    // Endpoint for validating user password
    [HttpPost("ValidateUserPassword")]
    public IActionResult ValidateUserPassword([FromBody] ImageAuthenticationModel model)
    {
        List<string>? password = null;
        try
        {
            // Validate user password
            ImageBusinessLayer imageBusinessLayer = new ImageBusinessLayer();
            ImageAuthenticationModel outModel = imageBusinessLayer.ValidateUser(model);

            // Return the validation result
            return Ok(new { Message = model.Message, Status = model.Status, FistName = model.FirstName, LastName = model.LastName });
        }
        catch (Exception ex)
        {
            // Return unauthorized status and error message
            return Unauthorized(new { Message = ex.Message });
        }
    }

    #endregion

    // Function to check if the user is valid (dummy implementation, always returns true)
    private bool IsValidUser(string EmailId, List<string> selectedImageNames)
    {
        return true;
    }
}
