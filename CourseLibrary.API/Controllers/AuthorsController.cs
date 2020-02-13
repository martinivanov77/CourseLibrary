using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseLibrary.API.Controllers
{

    [ApiController] //The controller does not need to check if ModelState.IsValid in case it is decorated with this attribute
    [Route("api/authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly ICourseLibraryRepository courseLibraryRepository;

        public AuthorsController(ICourseLibraryRepository courseLibraryRepository)
        {
            this.courseLibraryRepository = courseLibraryRepository;
        }

        [HttpGet]
        public IActionResult GetAuthors()
        {
            var authorsFromRepo = this.courseLibraryRepository.GetAuthors();
            return Ok(authorsFromRepo);
        }

        [HttpGet("{authorId}")]
        public IActionResult GetAuthor(Guid authorId)
        {
            var authorFromRepo = this.courseLibraryRepository.GetAuthor(authorId);

            if(authorFromRepo == null)
            {
                return NotFound();
            }

            return Ok(authorFromRepo);
        }
    }
}