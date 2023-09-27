
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public MessageRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddGroup(Group group)
        {
            _context.Groups.Add(group);

        }

        public void AddMessage(Message message)
        {
            _context.Messages.Add(message);
        }

        public void DeleteMessage(Message message)
        {
            _context.Messages.Remove(message);
        }

        public async Task<Connection> GetConnection(string connectionId)
        {
            return await _context.Connections.FindAsync(connectionId);
        }

        public async Task<Group> GetGroupFromConnection(string connectionId)
        {

            return await _context.Groups
             .Include(c => c.Connections)
             .Where(c => c.Connections.Any(x => x.ConnectionId == connectionId))
             .FirstOrDefaultAsync();
        }

        [HttpGet]

        public async Task<Message> GetMessage(int id)
        {
            return await _context.Messages.FindAsync(id);
        }

        public async Task<Group> GetMessageGroup(string groupName)
        {
            return await _context.Groups
            .Include(c => c.Connections)
            .FirstOrDefaultAsync(g => g.Name == groupName);
        }

        [HttpGet]
        public async Task<PageList<MessageDto>> GetMessagesForUser(MessageParams messageParams)
        {
            var query = _context.Messages
                .OrderByDescending(m => m.MessageSent)
                .AsQueryable();

            query = messageParams.Container switch
            {
                "Inbox" => query.Where(u => u.Recipient.UserName == messageParams.Username && u.RecipientDeleted == false),
                "Outbox" => query.Where(u => u.Sender.UserName == messageParams.Username && u.SenderDeleted == false),
                _ => query.Where(u => u.Recipient.UserName == messageParams.Username && u.RecipientDeleted == false && u.DateRead == null)
            };

            var messages = query.ProjectTo<MessageDto>(_mapper.ConfigurationProvider);

            return await PageList<MessageDto>.CreateAsync(messages, messageParams.PageNumber, messageParams.PageSize);


        }
        [HttpGet]
        //All messages between two users
        public async Task<IEnumerable<MessageDto>> GetMessageThread(string currentUserName, string recipientName)
        {
            var query = _context.Messages
                .Where(m =>
                    m.Recipient.UserName == currentUserName && m.RecipientDeleted == false && m.Sender.UserName == recipientName ||
                    m.Recipient.UserName == recipientName && m.SenderDeleted == false && m.Sender.UserName == currentUserName
                )
                .OrderBy(m => m.MessageSent)
                .AsQueryable();


            var unreadMessages = query.Where(m => m.DateRead == null && m.Recipient.UserName == currentUserName).ToList();
            if (unreadMessages.Any())
            {
                foreach (var message in unreadMessages)
                {
                    message.DateRead = DateTime.UtcNow;
                }

            }
            return await query.ProjectTo<MessageDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public void RemoveConnection(Connection connection)
        {
            _context.Connections.Remove(connection);
        }


    }
}