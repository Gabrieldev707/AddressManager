// AddressManager — interações de interface (front-end).
(function () {
    "use strict";

    /* ------------------------------------------------- Navbar (mobile) */
    const navToggle = document.getElementById("navToggle");
    const closeNav = () => {
        document.body.classList.remove("nav-open");
        if (navToggle) navToggle.setAttribute("aria-expanded", "false");
    };
    if (navToggle) {
        navToggle.addEventListener("click", () => {
            const open = document.body.classList.toggle("nav-open");
            navToggle.setAttribute("aria-expanded", open ? "true" : "false");
        });
    }
    document.addEventListener("keydown", (e) => { if (e.key === "Escape") closeNav(); });

    /* ------------------------------------------------------------- Alertas */
    document.querySelectorAll(".alert .alert-close").forEach((btn) => {
        btn.addEventListener("click", () => dismiss(btn.closest(".alert")));
    });
    document.querySelectorAll(".alert[data-autodismiss]").forEach((el) => {
        setTimeout(() => dismiss(el), 5000);
    });
    function dismiss(el) {
        if (!el) return;
        el.style.transition = "opacity .3s ease, transform .3s ease";
        el.style.opacity = "0";
        el.style.transform = "translateY(-6px)";
        setTimeout(() => el.remove(), 300);
    }

    /* ------------------------------------------------- Mostrar/ocultar senha */
    document.querySelectorAll("[data-pw-toggle]").forEach((btn) => {
        const input = document.getElementById(btn.getAttribute("data-pw-toggle"));
        if (!input) return;
        btn.addEventListener("click", () => {
            const show = input.type === "password";
            input.type = show ? "text" : "password";
            btn.setAttribute("aria-label", show ? "Ocultar senha" : "Mostrar senha");
            btn.classList.toggle("is-on", show);
        });
    });

    /* ------------------------------------------- Busca em tabela (cliente)
       Busca inteligente: case-insensitive, acento-insensitive e tolerante.
       Cobre logradouro, número, complemento, bairro, cidade, UF e CEP
       (este também por dígitos puros, ex.: "01001000" encontra "01001-000"). */
    const searchInput = document.querySelector("[data-search-input]");
    if (searchInput) {
        const table = document.getElementById(searchInput.getAttribute("data-search-target"));
        const rows = table ? Array.from(table.querySelectorAll("tbody tr[data-row]")) : [];
        const counter = document.getElementById("resultCount");
        const noResults = document.getElementById("noResults");
        const total = rows.length;

        // Normaliza: separa os acentos (NFD), remove as marcas diacríticas
        // (categoria Unicode "Mn" = combining marks), comprime espaços e baixa a caixa.
        const stripAccents = (str) => {
            const decomposed = (str || "").normalize("NFD");
            try {
                return decomposed.replace(/\p{Mn}/gu, "");
            } catch (e) {
                // Fallback para engines sem suporte a property escapes.
                return decomposed.replace(/[̀-ͯ]/g, "");
            }
        };
        const normalize = (str) => stripAccents(str).replace(/\s+/g, " ").trim().toLowerCase();

        // Pré-computa o texto pesquisável de cada linha uma única vez.
        const idx = rows.map((row) => {
            const text = normalize(row.textContent);
            return { row, text, digits: text.replace(/\D/g, "") };
        });

        const plural = (n) => n + " registro" + (n === 1 ? "" : "s");

        const run = () => {
            const q = normalize(searchInput.value);
            const qDigits = q.replace(/\D/g, "");
            let visible = 0;
            idx.forEach(({ row, text, digits }) => {
                let match = !q || text.includes(q);
                // Permite buscar CEP/número ignorando hífen e pontuação.
                if (!match && qDigits.length >= 2) {
                    match = digits.includes(qDigits);
                }
                row.hidden = !match;
                if (match) visible++;
            });
            if (counter) {
                counter.textContent = q
                    ? visible + " de " + plural(total)
                    : plural(total);
            }
            if (noResults) noResults.hidden = visible !== 0;
        };

        searchInput.addEventListener("input", run);
    }
})();
