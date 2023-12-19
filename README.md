# FinancialAudit
# Repository API audit application

Sistema de Auditoria Financeira
O Sistema de Gerenciamento Financeiro é uma aplicação construída em C# usando o framework ASP.NET Core. Ele oferece funcionalidades para o gerenciamento de usuários, transações financeiras e relatórios.

Funcionalidades

Usuários:

Criação de novos usuários.
Recuperação de informações de usuários existentes.
Atualização de informações do usuário.
Exclusão de usuários.
Transações:

Realização de depósitos, saques e compras.
Histórico de transações para cada usuário.
Consulta de saldo.
Relatórios:

Geração de relatórios de transações por usuário.
Visualização de balanço financeiro.
Pré-requisitos
Antes de começar, certifique-se de ter instalado:

.NET SDK - versão 8.0.
Git - para clonar o repositório.
Instalação
Clone o repositório:

git clone https://github.com/gabreuneto/financialAudit.git
Navegue até o diretório do projeto:

cd nome-do-projeto
Restaure as dependências:

dotnet restore
Configuração
O sistema utiliza DatatTable in memory. Para um banco de dados embutido, certifique-se de ajustar a configuração do banco de dados conforme necessário no arquivo de configuração (appsettings.json).

Uso
Execute a aplicação:

dotnet run
Acesse a API através do navegador ou de ferramentas como Swagger.

Contribuição
Contribuições são bem-vindas! Se encontrar problemas ou tiver sugestões, abra uma issue ou envie uma solicitação pull.

Licença
Este projeto está licenciado sob a Licença MIT. Consulte o arquivo LICENSE.md para obter mais detalhes.
