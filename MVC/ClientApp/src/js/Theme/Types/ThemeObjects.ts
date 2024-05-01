﻿import {Flow} from "../../Flow/FlowObjects";

export interface MainTheme {
    id: number;
    subject: string;
    flows: Flow[];
    themes: SubTheme[];
}

export interface SubTheme {
    id: number;
    subject: string;
    flows: Flow[];
    mainTheme: number;
}