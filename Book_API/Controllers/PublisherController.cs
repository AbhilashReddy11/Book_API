using AutoMapper;
using Book_API.Data;
using Book_API.Models.DTO;
using Book_API.Models;
using Book_API.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace Book_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherRepository _dbPublisher;
        private readonly IMapper _mapper;
        protected APIResponse _response;
      public PublisherController(IPublisherRepository dbPublisher, IMapper mapper)
        {
            _dbPublisher = dbPublisher;
            _mapper = mapper;
            _response = new();
           
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Publisher>>> GetPublishers()
        {
            try
            {
                IEnumerable<Publisher> publisherList = await _dbPublisher.GetAllAsync();

                _response.Result = _mapper.Map<List<Publisher>>(publisherList);
                _response.StatusCode = HttpStatusCode.OK;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_response);


        }
        [HttpGet("{id:int}", Name = "GetPublisher")]
        public async Task<ActionResult<APIResponse>> GetPublisher(int id)
        {
            try
            {

                if (id == 0)
                {

                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);

                }

                var publisher = await _dbPublisher.GetAsync(u => u.PID == id);
                if (publisher == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);


                }
                _response.Result = publisher;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;

        }
        [HttpPost]
        public async Task<ActionResult<APIResponse>> CreatePublisher([FromBody] PublisherCreateDTO createDTO)
        {
            try
            {
                if (await _dbPublisher.GetAsync(u => u.Name.ToLower() == createDTO.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Publisher already exists!");
                    return BadRequest(ModelState);
                }
                if (createDTO == null)
                {
                    return BadRequest();
                }

                Publisher publisher = _mapper.Map<Publisher>(createDTO);

                await _dbPublisher.CreateAsync(publisher);


                _response.Result = _mapper.Map<Publisher>(publisher);
                _response.StatusCode = HttpStatusCode.OK;
                return CreatedAtRoute("GetPublisher", new { id = publisher.PID }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> DeletePublisher(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }

                var publisher = await _dbPublisher.GetAsync(u => u.PID == id);
                if (publisher == null)
                {
                    return NotFound();
                }

                await _dbPublisher.RemoveAsync(publisher);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;

        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<APIResponse>> UpdateAuthor(int id, [FromBody] Publisher updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.PID)
                {
                    return BadRequest();
                }

                
                Publisher model = updateDTO;


                await _dbPublisher.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;

        }


    }
}
