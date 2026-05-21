import { Apresentador } from "./Apresentador";
import { Lote } from "./Lote";
import { RedeSocial } from "./RedeSocial";


export interface Evento{ 

    id : number;
    local : string;
    dataEvento : string;
    tema : string;
    qtdPessoas : number;
    imagemURL : string;
    telefone : string;
    email : string;
    descricao: string;
    modalidade: string;
    lotes : Lote[];
    redesSociais  : RedeSocial[];
    apresentadoresEventos : Apresentador[];
}
