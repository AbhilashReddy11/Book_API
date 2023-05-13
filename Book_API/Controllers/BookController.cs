using AutoMapper;
using Book_API.Data;
using Book_API.Models.DTO;
using Book_API.Models;
using Book_API.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace Book_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _dbBook;
        private readonly IAuthorRepository _dbAuthor;
        private readonly IPublisherRepository _dbPublisher;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        


        public BookController(IBookRepository dbBook, IMapper mapper, ApplicationDbContext db,IAuthorRepository dbAuthor, IPublisherRepository dbPublisher)
        {
            _dbBook = dbBook;
            _dbAuthor = dbAuthor;
            _dbPublisher = dbPublisher;
            _mapper = mapper;
            _response = new();
          
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks()
        {
            try
            {
                IEnumerable<Book> bookList = await _dbBook.GetAllAsync(includeProperties: "author,publisher");
                _response.Result = bookList;
                _response.StatusCode = HttpStatusCode.OK;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_response);


        }
        [HttpGet("{id:int}", Name = "GetBook")]
        public async Task<ActionResult<APIResponse>> GetBook(int id)
        {
            try
            {

                if (id == 0)
                {

                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);

                }

                var book = await _dbBook.GetAsync(u => u.ID == id, includeProperties : "author,publisher");
                if (book == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);


                }
                _response.Result = book;
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
        public async Task<ActionResult<APIResponse>> CreateBook([FromBody] BookCreateDTO createDTO)
        {
            try
            {
                if (await _dbBook.GetAsync(u => u.Title.ToLower() == createDTO.Title.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Book already exists!");
                    return BadRequest(ModelState);
                }
                if (await _dbAuthor.GetAsync(u => u.AID == createDTO.AuthorID) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "AuthorId is invalid");
                    return BadRequest(ModelState);
                }
                if (await _dbPublisher.GetAsync(u => u.PID == createDTO.PublisherID) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "PublisherId is invalid");
                    return BadRequest(ModelState);
                }
                if (createDTO == null)
                {
                    return BadRequest();
                }

                Book book = _mapper.Map<Book>(createDTO);

                await _dbBook.CreateAsync(book);


                _response.Result = _mapper.Map<Book>(book);
                _response.StatusCode = HttpStatusCode.OK;
                return CreatedAtRoute("GetBook", new { id = book.ID }, _response);
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
        public async Task<ActionResult<APIResponse>> DeleteBook(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }

                var book = await _dbBook.GetAsync(u => u.ID == id);
                if (book == null)
                {
                    return NotFound();
                }

                await _dbBook.RemoveAsync(book);
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
        public async Task<ActionResult<APIResponse>> UpdateBook(int id, [FromBody] BookDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.ID)
                {
                    return BadRequest();
                }
                if (await _dbAuthor.GetAsync(u => u.AID == updateDTO.AuthorID) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "AuthorID is invalid");
                    return BadRequest(ModelState);
                }
                if (await _dbPublisher.GetAsync(u => u.PID == updateDTO.PublisherID) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "PublisherID is invalid");
                    return BadRequest(ModelState);
                }

                 Book model = _mapper.Map<Book>(updateDTO);

                
                await _dbBook.UpdateAsync(model);
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
