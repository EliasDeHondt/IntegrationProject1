﻿/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using Data_Access_Layer;
using Domain.ProjectLogics;
using Domain.ProjectLogics.Steps;
using Domain.ProjectLogics.Steps.Information;
using Domain.ProjectLogics.Steps.Questions;

namespace Business_Layer;

public class StepManager
{

    private readonly StepRepository _repo;
    public StepManager(StepRepository repo)
    {
        _repo = repo;
    }
    
    public StepBase GetStepForFlowByNumber(long flowId, int stepNumber)
    {
        return _repo.ReadStepForFlowByNumber(flowId, stepNumber);
    }
    
    public StepBase? GetStepById(long? stepId)
    {
        return _repo.ReadStepById(stepId);
    }
    
    public Flow GetFlowByNumber(long flowId)
    {
        return _repo.ReadFlowByNumber(flowId);
    }
    
    public void ChangeStep(StepBase step)
    {
        _repo.UpdateStep(step);
    }

    public IEnumerable<StepBase> GetAllStepsForFlow(long flowId)
    {
        return _repo.ReadAllStepsForFlow(flowId);
    }

    public Choice CreateChoice(long flowId, int stepNr)
    {
        var step = _repo.ReadStepForFlowByNumber(flowId, stepNr);

        Choice choice;

        switch (step)
        {
            case QuestionStep questionStep:
                switch (questionStep.QuestionBase)
                {
                    case ChoiceQuestionBase choiceQuestion:
                        choice = new Choice(" ", choiceQuestion);
                        _repo.AddChoice(choiceQuestion, choice);
                        return choice;
                }
                break;
        }

        return null;
    }
}