using Microsoft.VisualStudio.TestTools.UnitTesting;
using WSUsuariosTurnero.Controllers;
using WSUsuariosTurnero.Models;

namespace PruebasUnitariasUsuariosTurnero
{
    [TestClass]
    public class UsuarioTest
    {
        private readonly ApplicationDbContext context;
        
        public UsuarioTest(ApplicationDbContext context)
        {
            this.context = context;
        }

        //[TestInitialize]
        //public void MetodoInicial()
        //{
        //    //Usuario usuario = new Usuario();
        //    //UsuarioController usuarioController = new UsuarioController(context);
        //    //usuarioController.ObtenerUsuario();

        //}
        //[TestMethod]
        //public void exiteUsuario()
        //{
        //}
    }
}
