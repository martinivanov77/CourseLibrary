using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CourseLibrary.API.Helpers;
using CourseLibrary.API.Models;
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
        private readonly IMapper mapper;

        public AuthorsController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            this.courseLibraryRepository = courseLibraryRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public ActionResult<IEnumerable<AuthorDto>> GetAuthors(string mainCategory, string searchQuery)
        {
            var authorsFromRepo = this.courseLibraryRepository.GetAuthors(mainCategory, searchQuery);

            //Mapping from repository to Dto Model
           
            return Ok(mapper.Map<IEnumerable<AuthorDto>>(authorsFromRepo));
        }

        [HttpGet("{authorId}")]
        public IActionResult GetAuthor(Guid authorId)
        {
            var authorFromRepo = this.courseLibraryRepository.GetAuthor(authorId);

            if(authorFromRepo == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<AuthorDto>(authorFromRepo));
        }
    }
}