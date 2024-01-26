// ImageAuthenticationController.cs
using ImageAuthenticationSystem.BAL;
using ImageAuthenticationSystem.DAL;
using ImageAuthenticationSystem.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")]
public class ImageAuthenticationController : ControllerBase
{
    #region "API"

    [HttpPost("login")]
    public IActionResult Login([FromBody] ImageAuthenticationModel model)
    {
        if (IsValidUser(model.EmailId, model.SelectedImageNames))
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

    [HttpPost("newUser")]
    public IActionResult CreateNewUser([FromBody] ImageAuthenticationModel model)
    {
        string status;
        try
        {
            //User Created successful
            ImageBusinessLayer imageBusinessLayer = new ImageBusinessLayer();
            status = imageBusinessLayer.CreateUser(model);
            return Ok(new { Message = status, UserName = model.EmailId });
        }
        catch (Exception ex)
        {
            return Unauthorized(new { Message = ex.Message });
        }
    }

    [HttpPost("getUserPassword")]
    public IActionResult GetUserPassword([FromBody] ImageAuthenticationModel model)
    {
        List<string>? password = null;
        try
        {
            //User Created successful
            ImageBusinessLayer imageBusinessLayer = new ImageBusinessLayer();
            password = new List<string>(imageBusinessLayer.GetUserPassword(model));
            return Ok(new { lstPassword = password });
        }
        catch (Exception ex)
        {
            return Unauthorized(new { Message = ex.Message });
        }
    }

    [HttpPost("ValidateUserPassword")]
    public IActionResult ValidateUserPassword([FromBody] ImageAuthenticationModel model)
    {
        List<string>? password = null;
        try
        {
            ImageBusinessLayer imageBusinessLayer = new ImageBusinessLayer();
            imageBusinessLayer.ValidateUser(model);

            password = new List<string>(imageBusinessLayer.GetUserPassword(model));
            return Ok(new { lstPassword = password });
        }
        catch (Exception ex)
        {
            return Unauthorized(new { Message = ex.Message });
        }
    }

    #endregion

    private bool IsValidUser(string EmailId, List<string> selectedImageNames)
    {
        return true;
    }
}
