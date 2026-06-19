// Integração com a API ViaCEP (https://viacep.com.br/).
// Ao informar um CEP válido no formulário de endereço, consulta a API e
// preenche automaticamente logradouro, bairro, cidade e UF.
(function () {
    "use strict";

    const cepInput = document.getElementById("CEP");
    if (!cepInput) {
        return;
    }

    const campos = {
        logradouro: document.getElementById("Logradouro"),
        bairro: document.getElementById("Bairro"),
        cidade: document.getElementById("Cidade"),
        uf: document.getElementById("UF"),
        complemento: document.getElementById("Complemento"),
        numero: document.getElementById("Numero")
    };

    const feedback = document.getElementById("cep-feedback");

    function soDigitos(valor) {
        return (valor || "").replace(/\D/g, "");
    }

    // Aplica a máscara 00000-000 enquanto o usuário digita.
    function aplicarMascara(valor) {
        const d = soDigitos(valor).slice(0, 8);
        return d.length > 5 ? d.slice(0, 5) + "-" + d.slice(5) : d;
    }

    function definirFeedback(mensagem, tipo) {
        if (!feedback) {
            return;
        }
        feedback.textContent = mensagem;
        const estado = tipo === "erro" ? " is-error"
            : tipo === "loading" ? " is-loading"
            : tipo === "ok" ? " is-ok" : "";
        feedback.className = "hint" + estado;
    }

    async function buscarCep() {
        const cep = soDigitos(cepInput.value);
        if (cep.length !== 8) {
            return;
        }

        definirFeedback("Buscando endereço...", "loading");
        try {
            const resposta = await fetch(`https://viacep.com.br/ws/${cep}/json/`);
            if (!resposta.ok) {
                throw new Error("Resposta inválida do serviço.");
            }

            const dados = await resposta.json();
            if (dados.erro) {
                definirFeedback("CEP não encontrado.", "erro");
                return;
            }

            preencher(campos.logradouro, dados.logradouro);
            preencher(campos.bairro, dados.bairro);
            preencher(campos.cidade, dados.localidade);
            preencher(campos.uf, dados.uf);
            // Só sugere complemento se o usuário ainda não preencheu.
            if (campos.complemento && !campos.complemento.value) {
                campos.complemento.value = dados.complemento || "";
            }

            definirFeedback("Endereço preenchido automaticamente.", "ok");
            if (campos.numero) {
                campos.numero.focus();
            }
        } catch (erro) {
            definirFeedback("Não foi possível consultar o CEP. Preencha manualmente.", "erro");
        }
    }

    function preencher(campo, valor) {
        if (campo) {
            campo.value = valor || "";
        }
    }

    cepInput.addEventListener("input", function () {
        cepInput.value = aplicarMascara(cepInput.value);
    });
    cepInput.addEventListener("blur", buscarCep);
})();
