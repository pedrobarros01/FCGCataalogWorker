using FCG.Catalog.Worker.Domain.Entities;
using FCG.Catalog.Worker.Domain.Exceptions;
using FCG.Catalog.Worker.Domain.Interfaces;
using FCG.Catalog.Worker.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Worker.Domain.Services;

public class LibraryDomainService : ILibraryDomainService
{
    private readonly ILibraryRepository _libraryRepository;
    private readonly IGameRepository _gameRepository;

    public LibraryDomainService(ILibraryRepository libraryRepository, IGameRepository gameRepository)
    {
        _libraryRepository = libraryRepository;
        _gameRepository = gameRepository;
    }

    public async Task<Library> AddGame(Library library, Guid gameId, long orderGameId)
    {
        Game game = await _gameRepository.GetById(gameId) ?? throw new BusinessException("Jogo não encontrado");
        if (game.Id != orderGameId) throw new BusinessException("Jogo pedido não é o mesmo da entidade GameOrder");
        library.AddGame(game);
        return library;
    }

    public async Task<Library> GetByUserId(Guid userId)
    {
        Library library = await _libraryRepository.GetLibraryByUserId(userId) ?? throw new BusinessException("Biblioteca não encontrada");
        return library;
    }

    public async Task<bool> LibraryExist(Guid userId) => await _libraryRepository.GetLibraryByUserId(userId) != null;
}
