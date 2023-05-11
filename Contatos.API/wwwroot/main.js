window.onload = async function () {
    AdicionarEventoParaFecharModalAoClicarFora();

    await getPessoas();
}

function AdicionarEventoParaFecharModalAoClicarFora() {
    window.addEventListener("click", (event) => {
        if (event.target == document.getElementById("editarPessoaModal")) {
            document.getElementById("editarPessoaModal").style.display = "none";
        }

        if (event.target == document.getElementById("editarContatoModal")) {
            document.getElementById("editarContatoModal").style.display = "none";
        }

        if (event.target == document.getElementById("adicionarPessoaModal")) {
            document.getElementById("adicionarPessoaModal").style.display = "none";
        }

        if (event.target == document.getElementById("adicionarContatoModal")) {
            document.getElementById("adicionarContatoModal").style.display = "none";
        }
    });
}

let pessoas = [];
async function getPessoas() {
    await fetch("https://listacontatosmuriel.azurewebsites.net/api/pessoas")
    .then(response => response.json())
    .then(response => {
        pessoas = response;
        AtualizarTabelaPessoas();
    })
}

async function postPessoa(inputModel) {
    await fetch('https://listacontatosmuriel.azurewebsites.net/api/pessoas', {
        method: 'POST',
        body: JSON.stringify(inputModel),
        headers: {
            'Content-Type': 'application/json'
        }
    })
    .then(response => response.json())
    .then(response => {
        const pessoa = {
            id: response.id,
            nome: inputModel.nome,
            contatos: []
        };

        pessoas.push(pessoa)
        AtualizarTabelaPessoas();
    })
}

async function putPessoa(pessoaId, inputModel) {
    await fetch(`https://listacontatosmuriel.azurewebsites.net/api/pessoas/${pessoaId}`, {
        method: 'PUT',
        body: JSON.stringify(inputModel),
        headers: {
            'Content-Type': 'application/json'
        }
    })
    .then(response => {
        pessoas.find(pessoa => pessoa.id == pessoaId).nome = inputModel.nome;

        AtualizarTabelaPessoas();
    })
}

async function deletePessoa(pessoaId) {
    await fetch(`https://listacontatosmuriel.azurewebsites.net/api/pessoas/${pessoaId}`, {
        method: 'DELETE'
    })
    .then(response => {
        pessoas = pessoas.filter(pessoa => pessoa.id != pessoaId);

        AtualizarTabelaPessoas();
    })
}

async function postContato(pessoaId, inputModel) {
    await fetch(`https://listacontatosmuriel.azurewebsites.net/api/pessoas/${pessoaId}/contatos`, {
        method: 'POST',
        body: JSON.stringify(inputModel),
        headers: {
            'Content-Type': 'application/json'
        }
    })
    .then(response => response.json())
    .then(response => {
        const contato = {
            nome: inputModel.nome,
            tipo: inputModel.tipo,
            valor: inputModel.valor,
            pessoaId: pessoaId,
            id: response.id
        };

        const pessoa = pessoas.find(pessoa => pessoa.id == pessoaId);
        pessoa.contatos.push(contato);

        AtualizarTabelaPessoas();
        AtualizarTabelaContatos(pessoa);
    })
}

async function putContato(pessoaId, contatoId, inputModel) {
    await fetch(`https://listacontatosmuriel.azurewebsites.net/api/pessoas/${pessoaId}/contatos/${contatoId}`, {
        method: 'PUT',
        body: JSON.stringify(inputModel),
        headers: {
            'Content-Type': 'application/json'
        }
    })
    .then(response => {
        const pessoa = pessoas.find(pessoa => pessoa.id == pessoaId);
        const contato = pessoa.contatos.find(contato => contato.id == contatoId);

        contato.nome = inputModel.nome;
        contato.tipo = inputModel.tipo;
        contato.valor = inputModel.valor;

        AtualizarTabelaContatos(pessoa);
    })
}

async function deleteContato(pessoaId, contatoId) {
    await fetch(`https://listacontatosmuriel.azurewebsites.net/api/pessoas/${pessoaId}/contatos/${contatoId}`, {
        method: 'DELETE'
    })
    .then(response => {
        const pessoa = pessoas.find(pessoa => pessoa.id == pessoaId);
        pessoa.contatos = pessoa.contatos.filter(contato => contato.id != contatoId);

        AtualizarTabelaContatos(pessoa);
    })
}


function AtualizarTabelaPessoas() {
    const pessoaTableBody = document.getElementById("pessoa-table").querySelector("tbody");
    pessoaTableBody.innerHTML = '';

    pessoas.forEach(pessoa => {
        AdicionarPessoaNaTabela(pessoa);
    });
}

async function AdicionarPessoa(event) {
    event.preventDefault();

    const pessoaForm = event.target;

    const inputModel = {
        nome: pessoaForm.elements["pessoaNome"].value
    };
    await postPessoa(inputModel);

    pessoaForm.reset();

    document.getElementById("adicionarPessoaModal").style.display = "none";
}

async function AdicionarPessoaNaTabela(pessoa) {
    const pessoaTabela = document.getElementById("pessoa-table").querySelector("tbody");

    const novaLinha = pessoaTabela.insertRow();
    novaLinha.setAttribute("pessoa-id", pessoa.id)

    const celulaId = novaLinha.insertCell(0);
    const celulaNome = novaLinha.insertCell(1);
    const celulaContatos = novaLinha.insertCell(2);
    const celulaAcoes = novaLinha.insertCell(3);

    celulaId.textContent = pessoa.id;
    celulaNome.textContent = pessoa.nome;
    celulaContatos.textContent = pessoa.contatos.length;

    const editarPessoaBotao = document.createElement("button");
    editarPessoaBotao.textContent = "Editar";
    editarPessoaBotao.addEventListener("click", () => {
        const editarPessoaForm = document.getElementById("editarPessoaForm");

        editarPessoaForm.elements.editarPessoaId.value = pessoa.id;
        editarPessoaForm.elements.editarPessoaNome.value = pessoa.nome;

        AtualizarTabelaContatos(pessoa);

        document.getElementById("editarPessoaModal").style.display = "block";
    });

    const deletarPessoaBotao = document.createElement("button");
    deletarPessoaBotao.textContent = "Deletar";
    deletarPessoaBotao.addEventListener("click", async () => {
        if (confirm(`Você realmente deseja deletar o(a) ${pessoa.nome}?`)) {
            await deletePessoa(pessoa.id);
        }
    });

    celulaAcoes.appendChild(editarPessoaBotao);
    celulaAcoes.appendChild(deletarPessoaBotao);
}

async function SalvarAlteracoesPessoa(event) {
    event.preventDefault();

    const editarPessoaForm = event.target;

    const pessoaId = editarPessoaForm.elements.editarPessoaId.value;
    const pessoaNome = editarPessoaForm.elements.editarPessoaNome.value;

    const inputModel = {
        nome: pessoaNome
    };
    await putPessoa(pessoaId, inputModel);

    document.getElementById("editarPessoaModal").style.display = "none";
}


function AtualizarTabelaContatos(pessoa) {
    const pessoaContatosTabela = document.getElementById("pessoa-contatos-table").querySelector("tbody");
    pessoaContatosTabela.innerHTML = '';

    pessoa.contatos.forEach(contato => {
        AdicionarContatoNaTabela(contato);
    });
}

async function AdicionarContato(event) {
    event.preventDefault();

    const contatoForm = event.target;

    const pessoaId = document.getElementById('editarPessoaForm').elements.editarPessoaId.value;
    const inputModel = {
        pessoaId: pessoaId,
        nome: contatoForm.elements["contatoNome"].value,
        tipo: parseInt(contatoForm.elements["contatoTipo"].value),
        valor: contatoForm.elements["contatoValor"].value
    }
    await postContato(pessoaId, inputModel);

    contatoForm.reset();

    document.getElementById("adicionarContatoModal").style.display = "none";
}

async function AdicionarContatoNaTabela(contato) {
    const pessoaContatosTabela = document.getElementById("pessoa-contatos-table").querySelector("tbody");

    const novaLinha = pessoaContatosTabela.insertRow();
    novaLinha.setAttribute("contato-id", contato.id)

    const celulaNome = novaLinha.insertCell(0);
    const celulaTipo = novaLinha.insertCell(1);
    const celulaValor = novaLinha.insertCell(2);
    const celulaAcoes = novaLinha.insertCell(3);

    celulaNome.textContent = contato.nome;
    celulaTipo.textContent = ConverterContatoTipo(contato.tipo);
    celulaValor.textContent = contato.valor;

    const editarContatoBotao = document.createElement("button");
    editarContatoBotao.textContent = "Editar";
    editarContatoBotao.addEventListener("click", () => {
        const editarContatoForm = document.getElementById("editarContatoForm");

        editarContatoForm.elements.editarContatoId.value = contato.id;
        editarContatoForm.elements.editarContatoNome.value = contato.nome;
        editarContatoForm.elements.editarContatoTipo.value = contato.tipo;
        editarContatoForm.elements.editarContatoValor.value = contato.valor;

        document.getElementById("editarContatoModal").style.display = "block";
    });

    const deletarContatoBotao = document.createElement("button");
    deletarContatoBotao.textContent = "Deletar";
    deletarContatoBotao.addEventListener("click", async (event) => {
        if (confirm(`Você realmente deseja deletar o contato ${contato.nome}?`)) {
            await deleteContato(contato.pessoaId, contato.id);
        }
    });

    celulaAcoes.appendChild(editarContatoBotao);
    celulaAcoes.appendChild(deletarContatoBotao);
}

async function SalvarAlteracoesContato(event) {
    event.preventDefault();

    const contatoForm = event.target;

    const pessoaId = document.getElementById('editarPessoaForm').elements.editarPessoaId.value;
    const contatoId = contatoForm.elements["editarContatoId"].value;
    const inputModel = {
        nome: contatoForm.elements["editarContatoNome"].value,
        tipo: parseInt(contatoForm.elements["editarContatoTipo"].value),
        valor: contatoForm.elements["editarContatoValor"].value
    }
    await putContato(pessoaId, contatoId, inputModel);

    document.getElementById("editarContatoModal").style.display = "none";

    contatoForm.reset();
}

function ConverterContatoTipo(contatoTipo) {
    switch (contatoTipo) {
        case 1:
            return "Telefone"
        case 2:
            return "Email"
        case 3:
            return "Whatsapp"
        default:
            return "";
    }
}