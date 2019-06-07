using System;
using System.Collections.Generic;
using System.Linq;
using LND.Data.Infrastructure;
using LND.Data.Repositories;
using LND.Model.Models;

namespace LND.Service
{
    public interface IPostCategoryService
    {
        PostCategory Add(PostCategory PostCategory);

        void Update(PostCategory PostCategory);

        PostCategory Delete(int id);

        IEnumerable<PostCategory> GetAll();

        IEnumerable<PostCategory> GetListPostByCategoryIdPaging(int page, int pageSize, out int totalRow);

        IEnumerable<PostCategory> GetAll(string keyword);

        IEnumerable<PostCategory> GetAllByParentId(int parentId);

        PostCategory GetById(int id);

        void Save();
    }

    public class PostCategoryService : IPostCategoryService
    {
        private IPostCategoryRepository _PostCategoryRepository;
        private IUnitOfWork _unitOfWork;

        public PostCategoryService(IPostCategoryRepository PostCategoryRepository, IUnitOfWork unitOfWork)
        {
            this._PostCategoryRepository = PostCategoryRepository;
            this._unitOfWork = unitOfWork;
        }

        public PostCategory Add(PostCategory PostCategory)
        {
            return _PostCategoryRepository.Add(PostCategory);
        }

        public PostCategory Delete(int id)
        {
            return _PostCategoryRepository.Delete(id);
        }

        public IEnumerable<PostCategory> GetAll()
        {
            return _PostCategoryRepository.GetAll();
        }

        public IEnumerable<PostCategory> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _PostCategoryRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            else
                return _PostCategoryRepository.GetAll();
        }

        public IEnumerable<PostCategory> GetAllByParentId(int parentId)
        {
            return _PostCategoryRepository.GetMulti(x => x.Status && x.ParentID == parentId);
        }

        public PostCategory GetById(int id)
        {
            return _PostCategoryRepository.GetSingleById(id);
        }

        public IEnumerable<PostCategory> GetListPostByCategoryIdPaging(int page, int pageSize, out int totalRow)
        {
            IEnumerable<PostCategory> query = _PostCategoryRepository.GetMulti(x => x.Status);
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(PostCategory PostCategory)
        {
            _PostCategoryRepository.Update(PostCategory);
        }
    }
}