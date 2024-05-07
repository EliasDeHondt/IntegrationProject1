﻿import {Modal} from "bootstrap";
import {GetFlows, GetTypeOfFlows} from "../Kiosk/FlowAPI";
import {GenerateOptions, SubmitFlows} from "./ChooseFlow";
import {connection, code} from "./Facilitator";

export const flowTypeModal = new Modal(document.getElementById('flowTypeModal')!, {
    keyboard: false,
    backdrop: "static"
});

const btnSubmitFlows = document.getElementById("btnSubmitFlows") as HTMLButtonElement;
const radioLinear = document.getElementById("radioLinear") as HTMLInputElement;
const radioCircular = document.getElementById("radioCircular") as HTMLInputElement;
const divFlows = document.getElementById("flowOptionsContainer") as HTMLDivElement;

if (btnSubmitFlows)
    btnSubmitFlows.onclick = () => {
        SubmitFlows(connection, code)
        flowTypeModal.hide();
    }


function FillOptions(type: string) {
    GetTypeOfFlows(type).then((flows) => {
        GenerateOptions(flows, divFlows, type)
    });
}

radioLinear.onchange = () => {
    FillOptions("linear");
} 

radioCircular.onchange = () => {
    FillOptions("circular");
} 