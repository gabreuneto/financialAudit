// using Microsoft.AspNetCore.Mvc;
// using System;
// using System.Collections.Generic;
// using System.Linq;

// [ApiController]
// [Route("api/[controller]")]
// public class AuditoriaController : ControllerBase
// {
//     private static List<User> usuarios = new List<User>();

//     [HttpGet("usuarios")]
//     public ActionResult<IEnumerable<User>> ObterUsuarios()
//     {
//         return Ok(usuarios);
//     }

//     [HttpPost("usuario")]
//     public ActionResult<User> CriarUsuario([FromBody] User usuario)
//     {
//         usuario.Id = usuarios.Count + 1;
//         usuarios.Add(usuario);
//         return CreatedAtAction(nameof(ObterUsuario), new { id = usuario.Id }, usuario);
//     }

//     [HttpGet("usuario/{id}")]
//     public ActionResult<User> ObterUsuario(int id)
//     {
//         var usuario = usuarios.FirstOrDefault(u => u.Id == id);
//         if (usuario == null)
//             return NotFound();

//         return Ok(usuario);
//     }

//     [HttpPost("transacao")]
//     public ActionResult RealizarTransacao([FromBody] Transaction transacao)
//     {
//         var usuario = usuarios.FirstOrDefault(u => u.Id == transacao.UsuarioId);
//         if (usuario == null)
//             return NotFound("Usuário não encontrado.");

//         // Atualiza o saldo do usuário com base na transação
//         switch (transacao.Type)
//         {
//             case TransactionType.Deposito:
//                 usuario.Saldo += transacao.Valor;
//                 break;
//             case TransactionType.Saque:
//                 if (usuario.Saldo < transacao.Valor)
//                     return BadRequest("Saldo insuficiente para saque.");

//                 usuario.Saldo -= transacao.Valor;
//                 break;
//             case TransactionType.Compra:
//                 // Lógica específica para compras
//                 break;
//         }

//         transacao.Id = usuario.Transactions.Count + 1;
//         transacao.Data = DateTime.Now;
//         usuario.Transactions.Add(transacao);

//         return Ok();
//     }

//     [HttpGet("saldo/{id}")]
//     public ActionResult<decimal> ObterSaldo(int id)
//     {
//         var usuario = usuarios.FirstOrDefault(u => u.Id == id);
//         if (usuario == null)
//             return NotFound("Usuário não encontrado.");

//         return Ok(usuario.Saldo);
//     }
// }
