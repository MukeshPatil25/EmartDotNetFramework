
using System.Web.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Description;
using E_Mart.Models;

namespace E_Mart.Controllers
{
    public class ValuesController : ApiController
    {
        private E_martdb db = new E_martdb();

      

        [HttpGet]
        public Object GetToken(int C_id)
        {
            string key = "my_secret_key_12345"; //Secret key which will be used later during validation    
            var issuer = "https://localhost:44377/api/";  //normally this will be your site URL    

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            
            //Create a List of Claims, Keep claims name short    
            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("C_id", C_id.ToString()));
           
          

            //Create Security Token object by giving required parameters    
            var token = new JwtSecurityToken(issuer, //Issure    
                            issuer,  //Audience    
                            permClaims,
        expires: DateTime.Now.AddDays(1),
        signingCredentials: credentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            return new { data = jwt_token };
        }


        [HttpPost]
        [Route("api/LoginUser/")]
        [ResponseType(typeof(Customer_DetailsController))]
        public IHttpActionResult PostLoginUser( [FromBody] Login login)
        {
            Object token;

            var au = db.Customer_Details.FirstOrDefault(u => u.Mobile_no == login.Mobile_no && u.Password == login.Password); ;

            if (au == null)
                return BadRequest();
            else
            {     
                 token = GetToken(au.c_id);
            }

            return Ok(token);
        }



    }
}
