﻿using Business_Layer;
using Domain.ProjectLogics.Steps;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class StepsController : Controller
{

    private readonly StepManager _manager;

    public StepsController(StepManager manager)
    {
        _manager = manager;
    }
    
    [HttpGet("GetNextStep/{stepNumber:int}/{flowId:long}")]
    public ActionResult GetNextStep(int stepNumber, long flowId)
    {
        IStep step = _manager.GetStepForFlowByNumber(flowId, stepNumber);
        if (step == null) return NoContent();
        return Ok(step);
    }
    
}