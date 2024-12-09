using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using mock.depart.Models;
using mock.depart.Services;

namespace mock.depart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatsController : ControllerBase
    {
        private readonly CatsService _service;

        public CatsController(CatsService service)
        {
            _service = service;
        }

        // *1 À faire si une action contient le code suivant User.FindFirstValue(ClaimTypes.NameIdentifier)
        // Propriété virtuelle pour obtenir l'ID de l'utilisateur courant
        // Cette propriété est virtuelle pour faciliter le mock lors des tests unitaires
        public virtual string UserId { get { return User.FindFirstValue(ClaimTypes.NameIdentifier)!; } }


        // Pour facilité les tests il vaut mieux utiliser un ActionResult<Type>
        // DELETE: api/Cats/5
        [HttpDelete("{id}")]
        public ActionResult<Cat> DeleteCat(int id)
        {
            // *1 UserId utilise une propriété pour pouvoir la "mock" facilement
            string? userid = UserId;

            // TODO vous devrez aussi faire un mock avec le service
            Cat? cat = _service.Get(id);
            if (cat == null)
            {
                return NotFound();
            }
            if (cat.CatOwner!.Id != userid)
            {
                return BadRequest("Cat is not yours");
            }
            if (cat.CuteLevel == Cuteness.BarelyOk)
            {
                cat = _service.Delete(id);
                return Ok(cat);
            }
            else
            {
                return BadRequest("Cat is too cute");
            }
        }
    }
}
