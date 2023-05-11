using AutoMapper;
using Book_API.Data;
using Book_API.Models;
using Book_API.Models.DTO;
using Book_API.Repository.IRepository;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Book_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    
         {


        private readonly IAuthorRepository _dbAuthor;
        private readonly IMapper _mapper;
        protected APIResponse _response;
       // private readonly ApplicationDbContext _db;


        public AuthorController(IAuthorRepository dbAuthor, IMapper mapper,ApplicationDbContext db)
        {
            _dbAuthor = dbAuthor;
            _mapper = mapper;
            _response = new();
         //   _db = db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAuthors()
        {
            try
            {
                IEnumerable<Author> authorList = await _dbAuthor.GetAllAsync();

                _response.Result = _mapper.Map<List<AuthorDTO>>(authorList);
                _response.StatusCode = HttpStatusCode.OK;
                
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_response);


        }
        [HttpGet("{id:int}",  Name = "GetAuthor")]
        // [Authorize(Roles = "admin")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
      


        public async Task<ActionResult<APIResponse>> GetAuthor(int id)
        {
            try { 
          
                if (id == 0)
                {

                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);

                }

                var author = await _dbAuthor.GetAsync(u => u.AID == id);
                if (author == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);


                }

                _response.Result = _mapper.Map<Author>(author);
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

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        // [Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> CreateAuthor([FromBody] AuthorCreateDTO createDTO)
        {
            try
            {
                if (await _dbAuthor.GetAsync(u => u.Name.ToLower() == createDTO.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Author already exists!");
                    return BadRequest(ModelState);
                }
                if (createDTO == null)
                {
                    return BadRequest();
                }

                Author author = _mapper.Map<Author>(createDTO);

                await _dbAuthor.CreateAsync(author);


                _response.Result = _mapper.Map<Author>(author);
                _response.StatusCode = HttpStatusCode.OK;
                return CreatedAtRoute("GetAuthor", new { id = author.AID }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> DeleteAuthor(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }

               var author = await _dbAuthor.GetAsync(u => u.AID == id);
                if (author == null)
                {
                    return NotFound();
                }

                await _dbAuthor.RemoveAsync(author);
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //  [Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> UpdateAuthor(int id, [FromBody] AuthorUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.AID)
                {
                    return BadRequest();
                }

                Author model = _mapper.Map<Author>(updateDTO);


                await _dbAuthor.UpdateAsync(model);
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
