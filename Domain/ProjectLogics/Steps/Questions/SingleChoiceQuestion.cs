﻿/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/


namespace Domain.ProjectLogics.Steps.Questions;

public class SingleChoiceQuestion : ChoiceQuestionBase
{

    public SingleChoiceQuestion(string question, ICollection<Choice> choices, ICollection<Answer> answers, long id = 0) : base(answers, question, choices, id)
    {
        Choices = choices;
    }
    
    public SingleChoiceQuestion(string question, ICollection<Choice> choices, long id = 0) : base(question, choices, id)
    {
        Choices = choices;
    }
    
    public SingleChoiceQuestion()
    {
        Choices = new List<Choice>();
    }
    
    private string SelectOne()
    {
        throw new NotImplementedException();
    }

    public override object Answer()
    {
        throw new NotImplementedException();
    }

}