using dbTransaction;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/transactions")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpPost()]
    public ActionResult PerformTransaction([FromBody] Transaction transaction)
    {
        // Logic to perform a transaction
        try
        {
            _transactionService.PerformTransaction(transaction);
            return Ok();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public ActionResult<decimal> GetBalance(int id)
    {
        // Logic to get the balance of a user
        try
        {
            var balance = _transactionService.GetBalance(id);
            return Ok(balance);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
     [HttpGet("report/{userId}")]
    public ActionResult<IEnumerable<Transaction>> ObterTransacoesUsuario(int userId)
    {
        try
        {
            var userTransac = _transactionService.GetUserTransactions(userId);
            return Ok(userTransac);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    
}
