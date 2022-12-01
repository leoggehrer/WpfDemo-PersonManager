using System.Text.Json;

namespace PersonManager.Logic.Repositories
{
    public abstract class Repository<TModel>
        where TModel : Models.ModelObject, Contracts.IIdentifyable, ICloneable
    {
        private List<TModel> modelList = new();

        private string FileName => $"{typeof(TModel).Name}.json";

        protected Repository()
        {
            Load();
        }
        public abstract TModel Create();
        public void Add(TModel model)
        {
            if (modelList.Any())
            {
                model.Id = modelList.Max(m => m.Id) + 1;
            }
            else
            {
                model.Id = 1;
            }
            modelList.Add(model);
        }

        public TModel? GetById(int id) => modelList.FirstOrDefault(m => m.Id == id)?.Clone() as TModel;
        public TModel[] GetAll()
        {
            return modelList.Where(m => m is TModel)
                            .Select(m => (m.Clone() as TModel)!)
                            .ToArray();
        }
        public bool Update(TModel model)
        {
            var listModel = modelList.FirstOrDefault(m => m.Id == model.Id);

            if (listModel != null)
            {
                modelList.Remove(listModel);
                modelList.Add(model);
            }
            return listModel != null;
        }

        public void Delete(int id)
        {
            var model = modelList.FirstOrDefault(m => m.Id == id);

            if (model != null)
            {
                modelList.Remove(model);
            }
        }

        public void Save()
        {
            var jsonData = JsonSerializer.Serialize<TModel[]>(modelList.ToArray());

            File.WriteAllText(FileName, jsonData);
        }

        internal void Load()
        {
            modelList.Clear();
            if (File.Exists(FileName))
            {
                var jsonData = File.ReadAllText(FileName);
                var models = JsonSerializer.Deserialize<TModel[]>(jsonData);

                if (models != null)
                {
                    modelList.AddRange(models);
                }
            }
        }
    }
}
