import { Evento } from "./Evento";
import { RedeSocial } from "./RedeSocial";

export interface Apresentador {

        id : number;
        nome  : string;
        miniCurriculo  : string;
        imagemURL : string;
        telefone  : number;
        email : string;
        redesSociais : RedeSocial[];
        apresentadoresEventos : Evento[];
}
