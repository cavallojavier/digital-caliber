export class measures {
    public hcNumber: string;
    public patientName: string;
    public mouth: mouth;
    public teeth: teeths;

    constructor() {
        this.mouth = new mouth;
        this.teeth = new teeths;
        this.hcNumber = '';
        this.patientName = '';
    }
}

class mouth{
    public leftSuperiorIncisive: number;
    public rightSuperiorIncisive: number;
    public leftSuperiorCanine: number;
    public rightSuperiorCanine: number;
    public leftSuperiorPremolar: number;
    public rightSuperiorPremolar: number;

    public leftInferiorIncisive: number;
    public rightInferiorIncisive: number;
    public leftInferiorCanine: number;
    public rightInferiorCanine: number;
    public leftInferiorPremolar: number;
    public rightInferiorPremolar: number;
}

class teeths{
    public tooth11: number;
    public tooth12: number;
    public tooth13: number;
    public tooth14: number;
    public tooth15: number;
    public tooth16: number;
    public tooth17: number;

    public tooth21: number;
    public tooth22: number;
    public tooth23: number;
    public tooth24: number;
    public tooth25: number;
    public tooth26: number;
    public tooth27: number;

    public tooth31: number;
    public tooth32: number;
    public tooth33: number;
    public tooth34: number;
    public tooth35: number;
    public tooth36: number;
    public tooth37: number;
    
    public tooth41: number;
    public tooth42: number;
    public tooth43: number;
    public tooth44: number;
    public tooth45: number;
    public tooth46: number;
    public tooth47: number;
}