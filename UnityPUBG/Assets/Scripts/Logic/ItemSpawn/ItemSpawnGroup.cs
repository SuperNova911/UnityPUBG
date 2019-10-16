﻿using UnityPUBG.Scripts.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityPUBG.Scripts.Utilities;

namespace UnityPUBG.Scripts
{
    public sealed class ItemSpawnGroup : MonoBehaviour
    {
        #region 필드
        [Header("Area Settings")]
        [SerializeField] private Vector3 areaSize = new Vector3(5, 1, 5);
        [SerializeField] ItemSpawnChance spawnChance = new ItemSpawnChance();

        [Header("Gizmo Settings")]
        [SerializeField] private Color areaColor = Color.cyan;
        [SerializeField] private bool showGroupArea = true;
        #endregion

        #region 속성
        /// <summary>
        /// 스폰 그룹의 아이템 스폰 확률 정보
        /// </summary>
        public ItemSpawnChance SpawnChance => spawnChance;
        /// <summary>
        /// 스폰 그룹의 범위
        /// </summary>
        public Bounds GroupArea => new Bounds(transform.position, areaSize);
        /// <summary>
        /// 스폰 그룹 Gizmo 색상
        /// </summary>
        public Color AreaColor => areaColor;
        #endregion

        #region 유니티 메시지
        private void Awake()
        {
            var spawnPoints = FindSpawnPointsInGroupArea();
            spawnPoints.ForEach(e => e.SpawnGroup = this);
        }

        private void OnDrawGizmos()
        {
            if (showGroupArea)
            {
                Gizmos.color = areaColor;
                Gizmos.DrawWireCube(GroupArea.center, GroupArea.size);
            }
        }
        #endregion

        #region 메서드
        /// <summary>
        /// 그룹 범위안에 있는 모든 ItemSpawnPoint의 리스트를 반환
        /// </summary>
        /// <returns>그룹 범위 안에 있는 ItemSpawnPoint 리스트</returns>
        private List<ItemSpawnPoint> FindSpawnPointsInGroupArea()
        {
            return GameObject.FindGameObjectsWithTag("ItemSpawnPoint")
                .Where(e => GroupArea.Contains(e.transform.position) && e.GetComponent<ItemSpawnPoint>() != null)
                .Select(e => e.GetComponent<ItemSpawnPoint>())
                .ToList();
        }
        #endregion
    }
}
