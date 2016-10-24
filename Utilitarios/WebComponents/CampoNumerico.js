//Pega um número: 989876,58
//E formata em: 989.876,58
function formataValor(obj) {
    obj.value = soNumero(obj.value);
    if (obj.value.length == 0) {
        obj.value = "0,00";
    }
    else if (obj.value.length < 3) {
        obj.value = obj.value + ",00";
    }
    else {
        formataValorDigitando(obj);
    }
}

function formataValorDigitando(obj, e) {
    valor = soNumero(obj.value);
    tam = valor.length;
    if (e != undefined && e.keyCode == '9') {
        obj.select();
        return;
    }
    if (tam < 3) {
        return;
    }
    else if (tam >= 3 && tam < 6) {
        obj.value = valor.substr(0, tam - 2) + "," + valor.substr(tam - 2);
    }
    else if (tam >= 6 && tam < 9) {
        obj.value = valor.substr(0, tam - 5) + "." + valor.substr(tam - 5, 3) + "," + valor.substr(tam - 2);
    }
    else if (tam >= 9 && tam < 12) {
        obj.value = valor.substr(0, tam - 8) + "." + valor.substr(tam - 8, 3) + "." + valor.substr(tam - 5, 3) + "," + valor.substr(tam - 2);
    }
    else if (tam >= 12 && tam < 15) {
        obj.value = valor.substr(0, tam - 11) + "." + valor.substr(tam - 11, 3) + "." + valor.substr(tam - 8, 3) + "." + valor.substr(tam - 5, 3) + "," + valor.substr(tam - 2);
    }
}

function formataCPFCNPJ(obj) {
    v = soNumero(obj.value);

    if (v.length > 11) {
        obj.value = formataCNPJ(obj.value);
    }
    else {
        obj.value = formataCPF(obj.value);
    }
}

function formataCNPJ(cnpj) {
    cnpj = soNumero(cnpj);

    if (cnpj.length == 0) {
        return "";
    }
    if (cnpj.length < 14) {
        dif = 14 - cnpj.length;
        for (i = 0; i < dif; i++) {
            cnpj = "0" + cnpj;
        }
    }
    return cnpj.substr(0, 2) + "." +
           cnpj.substr(2, 3) + "." +
           cnpj.substr(5, 3) + "/" +
           cnpj.substr(8, 4) + "-" +
           cnpj.substr(12, 2);
}

function formataCPF(cpf) {
    cpf = soNumero(cpf);

    if (cpf.length == 0) {
        return "";
    }
    if (cpf.length < 11) {
        dif = 11 - cpf.length;
        for (i = 0; i < dif; i++) {
            cpf = "0" + cpf;
        }
    }
    return cpf.substr(0, 3) + "." +
           cpf.substr(3, 3) + "." +
           cpf.substr(6, 3) + "-" +
           cpf.substr(9, 2);
}

function formataCEP(obj) {
    cep = soNumero(obj.value);

    if (cep.length == 0) {
        return "";
    }
    if (cep.length < 8) {
        dif = 8 - cep.length;
        for (i = 0; i < dif; i++) {
            cep = "0" + cep;
        }
    }
    obj.value = cep.substr(0, 5) + "-" + cep.substr(5, 3);
}

function formataDigito(obj) {
    txt = soNumero(obj.value);

    if (txt.length == 0) {
        return "";
    }
    obj.value = txt.substr(0, txt.length - 1) + "-" + txt.substr(txt.length - 1);
}

function formataRENAVAM(ren) {
    while (ren.length < 9)
        ren = '0' + ren;

    ren = soNumero(ren);

    return ren.substring(0, 3) + '.' +
           ren.substring(3, 6) + '.' +
           ren.substring(6, 9);
}

function formataMesAno(nome) {
    obj = document.getElementById(nome.id);
    v = soNumero(obj.value);
    if (v.length > 5) {
        obj.value = v.substr(0, 2) + '/' + v.substr(2, 4);
    }
}

function soNumero(str) {
    val = '';
    for (x = 0; x < str.length; x++) {
        if (str.charAt(x) == '0') val += str.charAt(x);
        else if (parseInt(str.charAt(x))) val += str.charAt(x);
    }
    return (val)
}

function pulaCampo(objOri, idObjDest, qtdeCaracteres, event) {
    pulaCampo(objOri, idObjDest, qtdeCaracteres, false, event);
}

function pulaCampo(objOri, idObjDest, qtdeCaracteres, soNum, event) {
    //se for as teclas de seta e as teclas Home e End, simplesmente retorna
    var tecla = event.keyCode;
    if (tecla == 8 || tecla == 9 || tecla == 16 || tecla == 17 || tecla == 46 ||
        tecla == 35 || tecla == 36 || tecla == 37 || tecla == 38 || tecla == 39 || tecla == 40)
        return;
    pulaCampoTemp = soNum ? soNumero(objOri.value) : objOri.value;
    if (pulaCampoTemp.length >= qtdeCaracteres && $get(idObjDest) != null && $get(idObjDest) != undefined
        && $('#' + idObjDest + ':visible').length > 0) {
        $get(idObjDest).focus();
    }
}

function validaMesAno(anomes, maximoAno) {
    var mesano = soNumero(anomes);
    if (mesano.length == 6) {
        var mes = mesano.substr(0, 2);
        var ano = mesano.substr(2, 4);
        return (mes >= 1 && mes <= 12) && (ano >= 1900 && ano <= maximoAno);
    }
    return false;
}

function validaMesAnoAteDataHoje(event, args) {
    args.IsValid = validaMesAno(args.Value, new Date().getFullYear());
    return args.IsValid;
}

function validaMesAnoUltimo(event, args) {
    args.IsValid = validaMesAno(args.Value, 2999);
    return args.IsValid;
}

function validaValorVazioZerado(event, args) {
    var v = soNumero(args.Value);

    args.IsValid = !(parseInt(v) == 0);
    return args.IsValid;
}