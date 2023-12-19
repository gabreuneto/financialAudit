# FinancialAudit
## Repository API audit application

**Sistema de Auditoria Financeira**

O Sistema de Gerenciamento Financeiro é uma aplicação construída em C# usando o framework ASP.NET Core. Ele oferece funcionalidades para o gerenciamento de usuários, transações financeiras e relatórios.

**Funcionalidades**

- **Usuários:**
  - Criação de novos usuários.
  - Recuperação de informações de usuários existentes.
  - Atualização de informações do usuário.
  - Exclusão de usuários.

- **Transações:**
  - Realização de depósitos, saques e compras.
  - Histórico de transações para cada usuário.
  - Consulta de saldo.

- **Relatórios:**
  - Geração de relatórios de transações por usuário.
  - Visualização de balanço financeiro.

**Pré-requisitos**

Antes de começar, certifique-se de ter instalado:

- [.NET SDK](https://dotnet.microsoft.com/download) - versão 8.0.
- [Git](https://git-scm.com/) - para clonar o repositório.

**Instalação**

1. **Clone o repositório:**

   ```bash
   git clone https://github.com/gabreuneto/financialAudit.git
   ```

2. **Navegue até o diretório do projeto:**

   ```
   cd nome-do-projeto
   ```

3. **Restaure as dependências:**

   ```bash
   dotnet restore
   ```

**Configuração**

O sistema utiliza DataTable in memory. Caso haja mudança para banco de dados embutido, deve-se configurar o arquivo (`appsettings.json`).

**Uso**

Execute a aplicação:

```bash
dotnet run
```

Acesse a API através do navegador ou de ferramentas como [Swagger](https://swagger.io/).

**Contribuição**

Contribuições são bem-vindas! Se encontrar problemas ou tiver sugestões, abra uma [issue](https://github.com/seu-usuario/seu-repositorio/issues) ou envie uma solicitação pull.

**Licença**

Este projeto está licenciado sob a [Licença MIT](LICENSE.md). Consulte o arquivo [LICENSE.md](LICENSE.md) para obter mais detalhes.
```
