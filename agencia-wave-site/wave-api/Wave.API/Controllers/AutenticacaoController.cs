﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wave.Application.Services.Interfaces;
using Wave.Domain.Account;
using Wave.Domain.Commands;
using Wave.Domain.Entities;
using Wave.Domain.Enums;
using Wave.Domain.Queries;

namespace Wave.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IUsuarioService _usuararioService;
        private readonly IPessoaService _pessoaService;

        public AutenticacaoController(IUsuarioService usuararioService,
                                      IPessoaService pessoaService)
        {
            _usuararioService = usuararioService;
            _pessoaService = pessoaService;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult<UsuarioQuery>> RegistrarUsuario([FromBody] RegistrarUsuarioCommand registrarUsuarioCommand)
        {
            var pessoaCommand = Pessoa.MapearCommandPessoa
            (
                registrarUsuarioCommand.Nome,
                registrarUsuarioCommand.Sobrenome,
                registrarUsuarioCommand.DataNascimento,
                registrarUsuarioCommand.CodigoGenero
            );
   
            var usuarioCommand = Usuario.MapearCommandUsuario
            (
                registrarUsuarioCommand.CodigoPessoa,
                registrarUsuarioCommand.NomeUsuario,
                registrarUsuarioCommand.Email,
                registrarUsuarioCommand.Telefone,
                registrarUsuarioCommand.Senha,
                registrarUsuarioCommand.SenhaConfirmada,
                registrarUsuarioCommand.Ativo,
                registrarUsuarioCommand.CodigoPerfil
            );

            var pessoaAdicionada = await _pessoaService.AdicionarPessoaAsync(pessoaCommand);

            if (pessoaAdicionada)
            {
                IEnumerable<PessoaQuery> listaPessoas = await _pessoaService.RecuperarListaPessoasAsync();
                var ultimaPessoa = listaPessoas.OrderByDescending(p => p.CodigoPessoa).FirstOrDefault();

                if (ultimaPessoa != null)
                {
                    usuarioCommand.CodigoPessoa = ultimaPessoa.CodigoPessoa;
                    usuarioCommand.CodigoPerfil = (int)PerfilEnum.Perfis.Usuario;
                    usuarioCommand.Ativo = true;

                    var usuarioAdicionado = await _usuararioService.AdicionarUsuarioAsync(usuarioCommand);
                    // Autenticação e autorização 
                    return Ok();
                }
                else
                {
                    return StatusCode(500, "Erro ao recuperar o código da pessoa adicionada.");
                }
            }
            else
            {
                return BadRequest("Erro ao adicionar a pessoa.");
            }
        }
    }
}
