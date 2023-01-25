# 🚗 PROJETO LOCADORA DE VEÍCULOS

Aplicação desenvolvida para atender a demanda de uma locadora de veículos, utilizando algumas regras e validações.

-----------------
### 📋 Requisitos

- Visual Studio Community 2022;
- MySql (Versão utilizada: 8.0.31).

-----------------
### 🔧 Configuração

Depois da instalação dos programas necessários, seguir os passos:

  - Clonar o projeto;
       ``` git@github.com:paulogabri-el/projetoLocadoraVeiculos.git ```
  
  - Configurar a string de conexão com o MySql no arquivo appsettings.json;
      ``` "server=nomedoserver;userid=usuario;password=senha;database=nomedobancodedados" ```
      
  - Salvar o projeto e rodar o comando "Update-Database" no console do gerenciador de pacotes;
  
      Uma vez feito, o EF já cria o banco de dados seguindo a string de conexão configurada.
  
  - Rodar os comandos presentes no .TXT abaixo no MySql para inserir uma pequena base incluindo o usuário admin para acesso ao sistema;
  
      [Scrip Inicial BD](https://github.com/paulogabri-el/projetoLocadoraVeiculos/files/10495278/ScriptLocadoraVec.txt)
  
  - Ao executar a aplicação você pode usar o seguinte usuário para login:
      ```
      Email: admin@admin.com.br
      Senha: useradmin
      ``` 
  
  - Ao acessar a aplicação, você consegue visualizar um botão "SOBRE A APLICAÇÃO", ele direciona para uma página listando as regras de negócio do projeto.

-----------------
## ✒️ Autor

* **Paulo Gabriel** - [Perfil GitHub](https://github.com/paulogabri-el) / [Perfil LinkedIn](https://www.linkedin.com/in/paulogabri-el/)

-----------------
## 📌 Extra

* Meu Primeiro Projeto;
* Projeto desenvolvido para uma avaliação para uma vaga de Dev. Jr;
* Proporcionou grande apredizado e ensinamentos para os demais projetos.


-----------------
