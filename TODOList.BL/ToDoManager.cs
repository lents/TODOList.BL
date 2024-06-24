namespace TODOList.BL
{
    public class ToDoManager
    {
        private readonly ITaskRepository _taskRepository;

        public ToDoManager(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public ToDoTask CreateTask(string title, string description)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title cannot be null or empty.", nameof(title));
            }

            var task = new ToDoTask
            {
                Title = title,
                Description = description,
                CreatedAt = DateTime.UtcNow
            };

            _taskRepository.Add(task);

            return task;
        }
    }

}
