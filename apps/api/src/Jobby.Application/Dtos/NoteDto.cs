﻿using Jobby.Application.Dtos.Base;

namespace Jobby.Application.Dtos;
public  record NoteDto : EntityDto
{
    public string Title { get; set; } 
    public string Description { get; set; } 
    public string JobReference { get; set; }
}
