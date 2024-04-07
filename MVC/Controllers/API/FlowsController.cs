using Business_Layer;
using Domain.ProjectLogics;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class FlowsController : Controller
{
    private readonly FlowManager _manager;

    public FlowsController(FlowManager manager)
    {
        _manager = manager;
    }
    
    [HttpGet("SetRespondentEmail/{flowId:int}/{inputEmail:string}")]
    public IActionResult SetRespondent(long flowId,string email)
    {
        _manager.SetParticipationByFlow(flowId,email);

        return Ok();

    [HttpPut("{id}/Paused")]
    public IActionResult PutFlowStateToPaused(long id)
    {
        Flow flow = _manager.GetFlowByIdWithTheme(id);

        if (flow == null)
            return NotFound();

        flow.State = FlowState.Paused;
        _manager.ChangeFlowState(flow);
        
        return NoContent();
    }

    [HttpPut("{id}/Active")]
    public IActionResult PutFlowStateToActive(long id)
    {
        Flow flow = _manager.GetFlowByIdWithTheme(id);

        if (flow == null)
            return NotFound();

        flow.State = FlowState.Active;
        _manager.ChangeFlowState(flow);
        
        return NoContent();
    }
}