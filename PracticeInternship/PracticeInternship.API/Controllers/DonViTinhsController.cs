using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeInternship.Application.DTOs;
using PracticeInternship.Application.DTOs.Conversions;
using PracticeInternship.Application.Interfaces;
using PracticeInternship.Application.Responses;

namespace PracticeInternship.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonViTinhsController(Interface_DM_Don_Vi_Tinh interface_DM_Don_Vi_Tinh) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DM_Don_Vi_Tinh_DTO>>> GetDonViTinhs()
        {
            // get all
            var don_Vi_Tinhs = await interface_DM_Don_Vi_Tinh.GetAllAsync();
            if (!don_Vi_Tinhs.Any())
            {
                return NotFound("No DM_Don_Vi_Tinh detected in the database");
            }

            var (_, list) = DM_Don_Vi_Tinh_Conversion.FromEntity(null, don_Vi_Tinhs);
            return list!.Any() ? Ok(don_Vi_Tinhs) : NotFound("No DM_Don_Vi_Tinh found");
        }

        [HttpPost]
        public async Task<ActionResult<Response>> CreateDonViTinh(DM_Don_Vi_Tinh_DTO dto)
        {
            // check model state is all data annotation are passed
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // convert entity
            var getEntity = DM_Don_Vi_Tinh_Conversion.ToEntity(dto);
            var response = await interface_DM_Don_Vi_Tinh.CreateAsync(getEntity);

            return response.Flag is true ? Ok(response) : BadRequest(response);
        }

        [HttpPut]
        public async Task<ActionResult<Response>> UpdateDonViTinh(DM_Don_Vi_Tinh_DTO dto)
        {
            // check model state is all data annotation are passed
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // convert to entity
            var getEntity = DM_Don_Vi_Tinh_Conversion.ToEntity(dto);
            var response = await interface_DM_Don_Vi_Tinh.UpdateAsync(getEntity);

            return response.Flag is true ? Ok(response) : BadRequest(response);
        }

        [HttpDelete]
        public async Task<ActionResult<Response>> DeleteDonViTinh(DM_Don_Vi_Tinh_DTO dto)
        {
            // convert to entity
            var getEntity = DM_Don_Vi_Tinh_Conversion.ToEntity(dto);
            var response = await interface_DM_Don_Vi_Tinh.DeleteAsync(getEntity);

            return response.Flag is true ? Ok(response) : BadRequest(response);
        }
    }
}
