//Atribui um MaxLength para os campos MultiLine
function MaxLengthMultiLine(obj, limit) {
  		if (obj.value.length >= limit) {
  		     event.keyCode = 0;
  		}
}

//Objetivo: Marcar todos os checkbox..
function SelecionaTodos(p_SelecionaTodos, p_Seleciona)
{
	var wrkForm=p_SelecionaTodos.form;
	var wrkCtAuxl=0;
	
	for(var wrkCtAuxl=0;wrkCtAuxl<wrkForm.length;wrkCtAuxl++)
	{	
	    if (wrkForm[wrkCtAuxl].name != null) {
		    if ( wrkForm[wrkCtAuxl].name.indexOf(p_Seleciona) > -1 )
		    {
			    var wrkSeleciona=wrkForm[wrkCtAuxl];
			    wrkSeleciona.checked=p_SelecionaTodos.checked;
		    }
		}
	}
}