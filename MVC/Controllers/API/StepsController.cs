﻿/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.Text.Json;
using System.Text.Json.Serialization;
using Business_Layer;
using Domain.ProjectLogics;
using Domain.ProjectLogics.Steps;
using Domain.ProjectLogics.Steps.Information;
using Domain.ProjectLogics.Steps.Questions;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers.API;

[ApiController]
[Route("api/[controller]")]
public class StepsController : Controller
{
    private readonly StepManager _manager;
    private readonly UnitOfWork _uow;

    public StepsController(StepManager manager, UnitOfWork uow)
    {
        _manager = manager;
        _uow = uow;
    }

    [HttpGet("GetNextStep/{flowId:int}/{stepNumber:long}")]
    public ActionResult GetNextStep(int stepNumber, long flowId)
    {
        StepBase stepBase = _manager.GetStepForFlowByNumber(flowId, stepNumber);

        var stepViewModel = StepModelFactory.CreateStepViewModel<StepViewModel, StepBase>(stepBase);

        return Ok(stepViewModel);
    }

    [HttpGet("GetConditionalNextStep/{stepId:long}")]
    public ActionResult GetConditionalNextStep(long stepId)
    {
        StepBase stepBase = _manager.GetStepById(stepId);

        var stepViewModel = StepModelFactory.CreateStepViewModel<StepViewModel, StepBase>(stepBase);

        return Ok(stepViewModel);
    }

    [HttpPost("AddNote/{flowId}/{stepNr}/{note}")]
    public ActionResult AddNote(long flowId, int stepNr, string note)
    {
        _uow.BeginTransaction();

        var newNote = _manager.AddNote(flowId, stepNr, note);

        _uow.Commit();

        return Created("AddNote", newNote);
    }

    [HttpGet("GetStepsFromFlow/{flowId:long}")]
    public ActionResult GetStepsFromFlow(long flowId)
    {
        var steps = _manager.GetAllStepsForFlow(flowId);

        var stepViewModels = steps.Select(StepModelFactory.CreateStepViewModel<StepViewModel, StepBase>).ToList();

        return Ok(stepViewModels);
    }

    [HttpPut("UpdateInformationStep")]
    public ActionResult UpdateInformationStep(InformationStepViewModel model)
    {
        _uow.BeginTransaction();

        var step = _manager.GetStepById(model.Id);

        if (step is InformationStep infoStep)
        {
            infoStep.InformationBases = model.InformationViewModel
                .Select(infoViewModel =>
                {
                    var info = _manager.GetInformationById(infoViewModel.Id);
                    _manager.ChangeInformation(info, infoViewModel.Information);
                    return info;
                }).ToList();
        }

        _uow.Commit();

        return NoContent();
    }

    [HttpPut("UpdateQuestionStep")]
    public ActionResult UpdateQuestionStep(QuestionStepViewModel model)
    {
        _uow.BeginTransaction();

        var step = _manager.GetStepById(model.Id);

        if (step is QuestionStep questionStep)
        {
            var question = questionStep.QuestionBase;
            question.Question = model.QuestionViewModel.Question;
            if (question is ChoiceQuestionBase choiceQuestion)
            {
                if (model.QuestionViewModel.Choices != null)
                    choiceQuestion.Choices = model.QuestionViewModel.Choices.Select<ChoiceViewModel, Choice>(
                        choiceViewModel =>
                        {
                            var choice = _manager.GetChoiceById(choiceViewModel.Id);
                            _manager.ChangeChoice(choice, choiceViewModel.Text, choiceViewModel.NextStepId);
                            return choice;
                        }).ToList();
            }
        }

        _uow.Commit();

        return NoContent();
    }
}