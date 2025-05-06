using Microsoft.EntityFrameworkCore;
using RazorPagesZoo.Models;
using System;
using System.Threading.Tasks;

public class TaskRepositoryService
{
    private readonly ZoodbContext _context;

    public TaskRepositoryService(ZoodbContext context)
    {
        _context = context;
    }

    public async Task<bool> AssignTaskAsync(int taskId, long employeeId, string userId)
    {
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                // 1. Блокируем строку в таблице Tasks с помощью FOR UPDATE
                var task = await _context.Tasks
                    .FromSqlInterpolated($"SELECT * FROM zoo_keepers.task WHERE id_task = {taskId} FOR UPDATE")
                    .FirstOrDefaultAsync();

                if (task == null)
                {
                    // Задание не найдено
                    return false;
                }

                // 2. Проверяем, не взято ли задание другим пользователем

                var existingAssignment = await _context.JournalOfTasks
                    .AnyAsync(j => j.IdTask == taskId);

                if (existingAssignment)
                {
                    // Задание уже взято другим пользователем
                    return false;
                }

                // 3. Проверяем, не существует ли уже записи с таким же IdEmployee и IdTask
                var existingJournalEntry = await _context.JournalOfTasks
                    .FirstOrDefaultAsync(j => j.IdEmployee == employeeId && j.IdTask == taskId);

                if (existingJournalEntry != null)
                {
                    // Запись уже существует, обновляем её
                    existingJournalEntry.Status = "В процессе";
                    existingJournalEntry.StartDate = DateOnly.FromDateTime(DateTime.Now);
                }
                else
                {
                    // 4. Добавление новой записи в JournalOfTask
                    await _context.JournalOfTasks.AddAsync(new JournalOfTask
                    {
                        IdEmployee = employeeId,
                        IdTask = taskId,
                        IdentityUserID = userId,
                        StartDate = DateOnly.FromDateTime(DateTime.Now),
                        Status = "В процессе"
                    });
                }

                // 5. Удаляем задание из таблицы Tasks (или помечаем как "взятое")
                //_context.Tasks.Remove(task); // Или task.Status = "Взято";

                // 6. Сохраняем изменения
                await _context.SaveChangesAsync();

                // 7. Завершаем транзакцию
                await transaction.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Откатываем транзакцию в случае ошибки
                await transaction.RollbackAsync();
                // Логируем ошибку
                Console.WriteLine($"Ошибка при назначении задания: {ex.Message}");
                throw;
            }
        }
    }
}